<template>
  <v-row no-gutters>
    <v-col sm="2" class="scrollable">
      <v-list subheader>
        <v-list-item
          @click="getRoomDetails({ id: 0, isJoin: true, name: 'chatBot' })"
        >
          <v-list-item-content>
            <v-list-item-title>Chat Bot</v-list-item-title>
          </v-list-item-content>
          <v-list-item-action>
            <v-icon>mdi-chat</v-icon>
          </v-list-item-action>
        </v-list-item>
      </v-list>
      <v-list subheader>
        <v-subheader>Lobiler</v-subheader>
        <v-list-item
          :v-if="lobis.length > 0"
          v-for="(lobi, index) in lobis"
          v-bind:key="index"
          @click="getRoomDetails(lobi)"
        >
          <v-list-item-content>
            <v-list-item-title v-html="lobi.name"></v-list-item-title>
          </v-list-item-content>
          <v-list-item-action>
            <v-icon>mdi-chat</v-icon>
          </v-list-item-action>
        </v-list-item>
      </v-list>
    </v-col>
    <v-col
      :sm="selectedLobi ? (selectedLobi.id !== 0 ? '8' : '10') : '10'"
      style="position: relative;"
    >
      <div class="chat-container" v-on:scroll="onScroll" ref="chatContainer">
        <div
          class="message"
          v-for="(message, index) in messages"
          v-bind:key="index"
          :class="{ own: message.senderUserId == currentUser.userId }"
        >
          <div
            class="username"
            v-if="
              index > 0 &&
                messages[index - 1].senderUserId != message.senderUserId
            "
          >
            {{ message.senderUserName }}
          </div>
          <div class="username" v-if="index == 0">
            {{ message.senderUserName }}
          </div>
          <div style="margin-top: 5px"></div>
          <div class="content">
            <div v-html="message.message"></div>
          </div>
        </div>
      </div>
      <div class="typer" v-if="selectedLobi">
        <template v-if="selectedLobi.isJoin">
          <input
            type="text"
            placeholder="Type here..."
            v-on:keyup.enter="sendMessage"
            v-model="content"
          />
        </template>
        <template v-else>
          <span style="width:100%;text-align:center"
            >Lobiye katılmanız gerekmektedir</span
          >
        </template>
      </div>
      <div class="typer" style="justify-content:center" v-else>
        <span>Lobi Seçmeni gerekmektedir</span>
      </div>
    </v-col>
    <v-col
      sm="2"
      class="scrollable"
      v-if="selectedLobi ? (selectedLobi.id === 0 ? false : true) : false"
    >
      <v-list subheader>
        <v-subheader>Your Chats</v-subheader>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title v-if="selectedLobi.isJoin">
              <v-btn
                x-large
                color="red accent-4"
                dark
                @click="leaveLobi(selectedLobi)"
                >Lobiden ayrıl</v-btn
              >
            </v-list-item-title>
            <v-list-item-title v-if="!selectedLobi.isJoin">
              <v-btn
                x-large
                color="deep-purple accent-4"
                dark
                @click="joinLobi(selectedLobi)"
                >Lobiye Katıl</v-btn
              >
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item
          :v-if="lobiUsers.length > 0"
          v-for="(user, index) in lobiUsers"
          v-bind:key="index"
          :to="/chat/ + index"
        >
          <v-list-item-content>
            <v-list-item-title v-html="user.userName"></v-list-item-title>
          </v-list-item-content>
          <v-list-item-action>
            <v-icon>mdi-chat</v-icon>
          </v-list-item-action>
        </v-list-item>
      </v-list>
    </v-col>
  </v-row>
</template>

<script>
import { mapGetters, mapMutations } from "vuex";
import axios from "axios";
import * as signalR from "@aspnet/signalr";

export default {
  name: "Chat",
  computed: {
    ...mapGetters(["connection"]),
  },
  data() {
    return {
      content: "",
      chatMessages: [],
      lobis: [],
      selectedLobi: undefined,
      lobiUsers: [],
      currentRef: {},
      loading: false,
      totalChatHeight: 0,
    };
  },
  created() {
    this.fecthLobis();
  },
  mounted() {
    this.loadChat();
    this.$store.getters.connection.on("ReceiveMessage", (message) => {
      if (message.lobiId === this.selectedLobi?.id) {
        if (message.senderUserId === 1) {
          message.isOwner = true;
        } else {
          message.isOwner = false;
        }
        this.chatMessages.push(message);
        this.scrollToEnd();
      }
    });
    this.$store.getters.connection.on("NewUser", (lobiId) => {
      if (lobiId === this.selectedLobi?.id) {
        this.getRoomDetails(this.selectedLobi);
      }
    });
  },
  computed: {
    messages() {
      return this.chatMessages;
    },
    currentUser() {
      return this.$store.getters.userInfo;
    },
    onNewMessageAdded() {
      const that = this;
      let onNewMessageAdded = function(snapshot, newMessage = true) {
        let message = snapshot.val();
        message.key = snapshot.key;
        /*eslint-disable */
        var urlPattern = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/gi;
        /*eslint-enable */
        message.content = message.content
          .replace(/&/g, "&amp;")
          .replace(/</g, "&lt;")
          .replace(/>/g, "&gt;")
          .replace(/"/g, "&quot;")
          .replace(/'/g, "&#039;");
        message.content = message.content.replace(
          urlPattern,
          "<a href='$1'>$1</a>"
        );
        if (!newMessage) {
          that.chatMessages.unshift(that.processMessage(message));
          that.scrollTo();
        } else {
          that.chatMessages.push(that.processMessage(message));
          that.scrollToEnd();
        }
      };
      return onNewMessageAdded;
    },
  },
  watch: {
    "$route.params.id"(newId, oldId) {
      this.currentRef.off("child_added", this.onNewMessageAdded);
      this.loadChat();
    },
  },
  methods: {
    joinLobi(lobi, create) {
      if (
        this.$store.getters.connection.state ===
        signalR.HubConnectionState.Connected
      ) {
        this.$store.getters.connection.invoke(
          "JoinGroup",
          lobi.id,
          this.$store.getters.userInfo.userId
        );
      } else {
        this.$store.getters.connection
          .start()
          .then(() =>
            this.$store.getters.connection.invoke(
              "JoinGroup",
              lobi.id,
              this.$store.getters.userInfo.userId
            )
          );
      }

      if (!create) {
        this.selectedLobi = lobi;
        this.selectedLobi.isJoin = true;
        //this.fecthLobis();
      }
    },
    leaveLobi(lobi) {
      if (lobi.isJoin) {
        if (
          this.$store.getters.connection.state ===
          signalR.HubConnectionState.Connected
        ) {
          this.$store.getters.connection.invoke(
            "LeaveGroup",
            lobi.id,
            this.$store.getters.userInfo.userId
          );
        } else {
          this.$store.getters.connection
            .start()
            .then(() =>
              this.$store.getters.connection.invoke(
                "LeaveGroup",
                lobi.id,
                this.$store.getters.userInfo.userId
              )
            );
        }
        this.selectedLobi = lobi;
        this.selectedLobi.isJoin = false;
        //this.fecthLobis();
      }
    },
    getRoomDetails(lobi) {
      this.selectedLobi = lobi;
      if (lobi.id !== 0) {
        axios
          .get("Lobi/GetLobiUsers?lobiId=" + lobi.id, {
            headers: {
              "Content-type": "application/json",
              "Access-Control-Allow-Origin": "*",
              Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
            },
          })
          .then((response) => {
            console.log(response);
            this.lobiUsers = response.data;
          });
        axios
          .get("Lobi/GetLobiMessages?Id=" + lobi.id, {
            headers: {
              "Content-type": "application/json",
              "Access-Control-Allow-Origin": "*",
              Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
            },
          })
          .then((response) => {
            this.chatMessages = response.data;
            this.scrollToEnd();
          });
      } else {
        this.chatMessages = [];
      }
    },
    fecthLobis() {
      axios
        .get("Lobi/GetAllLobies", {
          headers: {
            "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            Authorization: `Bearer ${this.$store.getters.userInfo.token}`,
          },
        })
        .then((response) => {
          this.lobis = response.data;
          response.data.forEach((lobi) => {
            if (lobi.isJoin) {
              this.joinLobi(lobi, true);
            }
          });
        });
      //this.chatMessages = data;
    },
    loadChat() {
      this.totalChatHeight = this.$refs.chatContainer.scrollHeight;
      this.loading = false;
      if (this.id !== undefined) {
        this.chatMessages = [];
        let chatID = this.id;
      }
    },
    onScroll() {
      let scrollValue = this.$refs.chatContainer.scrollTop;
      const that = this;
      if (scrollValue < 100 && !this.loading) {
        this.totalChatHeight = this.$refs.chatContainer.scrollHeight;
        this.loading = true;
        let chatID = this.id;
        let currentTopMessage = this.chatMessages[0];
        if (currentTopMessage === undefined) {
          this.loading = false;
          return;
        }
      }
    },
    sendMessage() {
      if (
        this.selectedLobi?.id !== 0 &&
        this.selectedLobi?.name !== "chatLobi"
      ) {
        if (this.content !== "") {
          if (
            this.$store.getters.connection.state ===
            signalR.HubConnectionState.Connected
          ) {
            this.$store.getters.connection.invoke("SendMessage", {
              lobiId: this.selectedLobi.id,
              senderUserId: this.$store.getters.userInfo.userId,
              senderUserName: this.$store.getters.userInfo.userName,
              message: this.content,
            });
          } else {
            this.$store.getters.connection.start().then(() => {
              this.$store.getters.connection.invoke("SendMessage", {
                lobiId: this.selectedLobi.id,
                senderUserId: this.$store.getters.userInfo.userId,
                senderUserName: this.$store.getters.userInfo.userName,
                message: this.content,
              });
              this.scrollToEnd();
            });
          }
          this.content = "";
        }
      } else {
        //SignalR ile servera gönderirken büyük M ?
        this.chatMessages.push({
          senderUserId: this.$store.getters.userInfo.userId,
          senderUserName: this.$store.getters.userInfo.userName,
          message: this.content,
        });
        axios
          .create({
            baseURL: "http://52.229.54.243:8000",
          })
          .post(
            "/api",
            {
              msg: this.content,
            },
            {
              headers: {
                "Content-type": "application/json",
                "Access-Control-Allow-Origin": "*",
              },
            }
          )
          .then((answer) => {
            this.chatMessages.push({
              senderUserId: 0,
              senderUserName: "CHATBOT",
              message: answer.res,
            });
            this.content = "";
            this.scrollToEnd();
          });

        /*
        {
    "desc": "Başarılı.",
    "ques": "hi",
    "res": "Hi there, how can I help?",
    "time": "2020-06-05 11:45:14"
}
        */
      }
    },
    scrollToEnd() {
      this.$nextTick(() => {
        var container = this.$el.querySelector(".chat-container");
        container.scrollTop = container.scrollHeight;
      });
    },
    scrollTo() {
      this.$nextTick(() => {
        let currentHeight = this.$refs.chatContainer.scrollHeight;
        let difference = currentHeight - this.totalChatHeight;
        var container = this.$el.querySelector(".chat-container");
        container.scrollTop = difference;
      });
    },
  },
};
</script>

<style lang="sass">
.scrollable
  overflow-y: auto
  height: 80vh

.typer
  box-sizing: border-box
  display: flex
  align-items: center
  bottom: 0
  height: 4.9rem
  width: 100%
  background-color: #fff
  box-shadow: 0 -5px 10px -5px rgba(0, 0, 0, 0.2)

.typer input[type="text"]
  position: absolute
  left: 2.5rem
  padding: 1rem
  width: 80%
  background-color: transparent
  border: none
  outline: none
  font-size: 1.25rem

.chat-container
  box-sizing: border-box
  height: calc(100vh - 9.5rem)
  overflow-y: auto
  padding: 10px
  background-color: #f2f2f2

.message
  margin-bottom: 3px

.message.own
  text-align: right

.message.own .content
  background-color: lightskyblue

.chat-container .username
  font-size: 18px
  font-weight: bold

.chat-container .content
  padding: 8px
  background-color: lightgreen
  border-radius: 10px
  display: inline-block
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.2), 0 1px 1px 0 rgba(0, 0, 0, 0.14), 0 2px 1px -1px rgba(0, 0, 0, 0.12)
  max-width: 50%
  word-wrap: break-word

@media (max-width: 480px)
  .chat-container .content
    max-width: 60%
</style>

<!--
firebase
          .database()
          .ref("messages")
          .child(chatID)
          .child("messages")
          .orderByKey()
          .endAt(currentTopMessage.key)
          .limitToLast(20)
          .once("value")
          .then(function(snapshot) {
            let tempArray = [];
            snapshot.forEach(function(item) {
              tempArray.push(item);
            });
            if (tempArray[0].key === tempArray[1].key) return;
            tempArray.reverse();
            tempArray.forEach(function(child) {
              that.onNewMessageAdded(child, false);
            });
            that.loading = false;
          });
          -->
