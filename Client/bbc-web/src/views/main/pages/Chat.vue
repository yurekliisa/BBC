<template>
  <v-row no-gutters>
    <v-col sm="2" class="scrollable">
      <v-list subheader>
        <v-subheader>Your Chats</v-subheader>
        <v-list-item
          v-for="(chat, index) in [{ name: 'Oda 1' }]"
          v-bind:key="chat.name"
          :to="/chat/ + index"
        >
          <v-list-item-content>
            <v-list-item-title v-html="chat.name"></v-list-item-title>
          </v-list-item-content>
          <v-list-item-action>
            <v-icon>mdi-chat</v-icon>
          </v-list-item-action>
        </v-list-item>
      </v-list>
    </v-col>
    <v-col sm="8" style="position: relative;">
      <div class="chat-container" v-on:scroll="onScroll" ref="chatContainer">
        <div
          class="message"
          v-for="(message, index) in messages"
          v-bind:key="index"
          :class="{ own: message.user == username }"
        >
          <div
            class="username"
            v-if="index > 0 && messages[index - 1].user != message.user"
          >
            {{ message.user }}
          </div>
          <div class="username" v-if="index == 0">{{ message.user }}</div>
          <div style="margin-top: 5px"></div>
          <div class="content">
            <div v-html="message.content"></div>
          </div>
        </div>
      </div>
      <div class="typer">
        <input
          type="text"
          placeholder="Type here..."
          v-on:keyup.enter="sendMessage"
          v-model="content"
        />
      </div>
    </v-col>
    <v-col sm="2" class="scrollable">
      <v-list subheader>
        <v-subheader>Your Chats</v-subheader>
        <v-list-item
          avatar
          v-for="(chat, index) in [{ name: 'Oda 1' }]"
          v-bind:key="chat.name"
          :to="/chat/ + index"
        >
          <v-list-item-content>
            <v-list-item-title v-html="chat.name"></v-list-item-title>
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

export default {
  name: "Chat",
  components: {},
  data() {
    return {
      content: "",
      chatMessages: [],
      currentRef: {},
      loading: false,
      totalChatHeight: 0,
    };
  },
  created() {
    let data = [
      {
        user: "Ahmet",
        content: "afhadfhasdfhadfhafhasfdhasfhasfh",
      },
      {
        user: "İsa Yürekli",
        content: "ghlşsdgşlmfgmlşmsfghlşmsrthşlmsfgh",
      },
      {
        user: "fhafdh",
        content: "sdfhgsdfh",
      },
      {
        user: "Ahmet",
        content: "afhadfhasdfhadfhafhasfdhasfhasfh",
      },
      {
        user: "İsa Yürekli",
        content: "ghlşsdgşlmfgmlşmsfghlşmsrthşlmsfgh",
      },
      {
        user: "fhafdh",
        content: "sdfhgsdfh",
      },
      {
        user: "Ahmet",
        content: "afhadfhasdfhadfhafhasfdhasfhasfh",
      },
      {
        user: "İsa Yürekli",
        content: "ghlşsdgşlmfgmlşmsfghlşmsrthşlmsfgh",
      },
      {
        user: "fhafdh",
        content: "sdfhgsdfh",
      },
      {
        user: "Ahmet",
        content: "afhadfhasdfhadfhafhasfdhasfhasfh",
      },
      {
        user: "İsa Yürekli",
        content: "ghlşsdgşlmfgmlşmsfghlşmsrthşlmsfgh",
      },
      {
        user: "fhafdh",
        content: "sdfhgsdfh",
      },
      {
        user: "Ahmet",
        content: "afhadfhasdfhadfhafhasfdhasfhasfh",
      },
      {
        user: "İsa Yürekli",
        content: "ghlşsdgşlmfgmlşmsfghlşmsrthşlmsfgh",
      },
      {
        user: "fhafdh",
        content: "sdfhgsdfh",
      },
    ];
    this.chatMessages = data;
  },
  mounted() {
    this.loadChat();
  },
  computed: {
    messages() {
      return this.chatMessages;
    },
    username() {
      return "İsa Yürekli";
      //return this.$store.getters.user.username
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
    loadChat() {
      this.totalChatHeight = this.$refs.chatContainer.scrollHeight;
      this.loading = false;
      if (this.id !== undefined) {
        this.chatMessages = [];
        let chatID = this.id;
        // this.currentRef = firebase
        //   .database()
        //   .ref("messages")
        //   .child(chatID)
        //   .child("messages")
        //   .limitToLast(20);
        //this.currentRef.on("child_added", this.onNewMessageAdded);
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
      if (this.content !== "") {
        console.log(this.username);
        //this.$store.dispatch('sendMessage', { username: this.username, content: this.content, date: new Date().toString(), chatID: this.id })
        this.messages.push({
          user: "İsa Yürekli",
          content: this.content,
        });
        this.content = "";
        this.scrollToEnd();
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
