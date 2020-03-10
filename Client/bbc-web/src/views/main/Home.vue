<template>
  <v-app>
    <v-card class="overflow-hidden">
      <!--ToolBar-->
      <v-app-bar absolute color="white">
        <v-toolbar-title class="ml-0 pl-1 mr-1">
          <router-link
            :to="{ name: 'home'}"
            class="google-font"
            style="text-decoration:none; color: rgba(0,0,0,.87);"
          >BBC</router-link>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn
          v-for="(link, i) in links"
          :key="i"
          :to="link.to"
          class="ml-2 google-font hidden-sm-and-down"
          style="text-transform: capitalize;"
          @click="onClick($event, link)"
        >{{ link.text }}</v-btn>
      </v-app-bar>
      <!--ToolBar-->
      <!--Slider-->
      <v-carousel class="mt-12" cycle height="auto" hide-delimiter-background show-arrows-on-hover>
        <v-carousel-item v-for="(slide, i) in slides" :key="i">
          <v-sheet height="100%">
            <v-parallax height="400" :src="slide"></v-parallax>
          </v-sheet>
        </v-carousel-item>
      </v-carousel>
      <!--Slider-->
      <!--Container-->
      <v-container class="pa-0 mt-3">
        <v-layout wrap align-start justify-center row fill-height class="hidden-sm-and-down mb-4">
          <v-flex xs3 sm6 md6 lg4 v-for="(item,i) in eventsData" :key="i">
            <v-card :loading="item.isLoading" class="ma-2 pa-1 my-2">
              <v-img height="250" :src="item.kapakResim"></v-img>
              <v-card-title>{{item.name}}</v-card-title>
              <v-card-text>
                <v-row align="center" class="mx-0">
                  <v-rating
                    :value="item.point"
                    color="amber"
                    dense
                    half-increments
                    readonly
                    size="14"
                  ></v-rating>
                  <div class="grey--text ml-4">{{item.point}} ({{item.commentCount}})</div>
                </v-row>

                <div class="my-4 subtitle-1">
                  <v-menu
                    v-model="item.menu"
                    bottom
                    right
                    transition="scale-transition"
                    origin="top left"
                  >
                    <template v-slot:activator="{ on }">
                      <v-chip outlined v-on="on">
                        <v-avatar left>
                          <v-img src="https://cdn.vuetifyjs.com/images/john.png"></v-img>
                        </v-avatar>
                        {{item.actor.fullName}}
                      </v-chip>
                    </template>
                    <v-card width="300">
                      <v-list dark>
                        <v-list-item>
                          <v-list-item-avatar>
                            <v-img src="https://cdn.vuetifyjs.com/images/john.png"></v-img>
                          </v-list-item-avatar>
                          <v-list-item-content>
                            <v-list-item-title>{{item.actor.fullName}}</v-list-item-title>
                            <v-list-item-subtitle>{{item.actor.job}}</v-list-item-subtitle>
                          </v-list-item-content>
                          <v-list-item-action>
                            <v-btn icon @click="item.menu = false">
                              <v-icon>mdi-close-circle</v-icon>
                            </v-btn>
                          </v-list-item-action>
                        </v-list-item>
                      </v-list>
                      <v-list>
                        <v-list-item @click="() => {}">
                          <v-list-item-action>
                            <v-icon>mdi-briefcase</v-icon>
                          </v-list-item-action>
                          <v-list-item-subtitle>{{item.actor.email}}</v-list-item-subtitle>
                        </v-list-item>
                      </v-list>
                    </v-card>
                  </v-menu>
                </div>
                <div>{{item.summary}}</div>
              </v-card-text>

              <v-divider class="mx-4"></v-divider>
              <v-card-title>Malzemeler</v-card-title>

              <v-card-text>
                <v-chip-group
                  v-model="item.anaUrun"
                  active-class="deep-purple accent-4 white--text"
                  column
                >
                  <v-chip  v-for="(urun,j) in item.urunler" :key="j">{{urun}}</v-chip>
                </v-chip-group>
              </v-card-text>
              <v-card-actions>
                <v-btn text color="deep-purple accent-4" @click="reserve(i)">Devamını Oku</v-btn>
                <v-spacer></v-spacer>
                <v-btn icon v-if="!item.isLike" @click="like(i,false)">
                  <v-icon>mdi-heart</v-icon>
                </v-btn>
                <v-btn icon v-if="item.isLike" @click="like(i,true)">
                  <v-icon color="red">mdi-heart</v-icon>
                </v-btn>
                <v-btn icon>
                  <v-icon>mdi-share-variant</v-icon>
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
      <div class="text-center">
        <v-pagination
          v-model="page"
          :length="4"
          color="deep-purple accent-4"
          prev-icon="mdi-menu-left"
          next-icon="mdi-menu-right"
        ></v-pagination>
      </div>
      <!--Container-->
    </v-card>

    <v-card height="150px">
      <v-footer v-bind="localAttrs" :padless="true">
        <v-card flat tile width="100%" class="red lighten-1 text-center">
          <v-card-text class="white--text">
            <div style="line-height:36px;float:left;padding-bottom:10px;">
              <v-btn v-for="icon in icons" :key="icon" icon>
                <v-icon>{{ icon }}</v-icon>
              </v-btn>
            </div>
            <div style="line-height:36px;float:right;padding-bottom:10px;">
              {{ new Date().getFullYear() }} —
              <strong>Bana Bi' Chef</strong>
            </div>
          </v-card-text>
        </v-card>
      </v-footer>
    </v-card>
  </v-app>
</template>

<script>
import { mapGetters, mapMutations } from "vuex";
export default {
  name: "Home",
  computed: {
    localAttrs() {
      const attrs = {};
      if (this.variant === "default") {
        attrs.absolute = false;
        attrs.fixed = false;
      } else {
        attrs[this.variant] = true;
      }
      return attrs;
    }
  },
  data() {
    return {
      variant: "absolute",
      icons: ["mdi-home", "mdi-email", "mdi-instagram", "mdi-facebook"],
      page: 1,
      showLoader: true,
      links: [
        { text: "Ana Sayfa", to: "/", icon: "home" },
        { text: "Reçete ve Tarif", to: "/events", icon: "rounded_corner" },
        { text: "İş İlanları", to: "/team", icon: "group" },
        { text: "Login", to: "/about", icon: "toc" }
      ],
      slides: [
        "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/banner.jpg.jpg",
        "https://www.yemekyapmasanati.com/wp-content/uploads/2015/05/sebze-banner.jpg",
        "https://thebosporus.com/wp-content/themes/bosporus_theme/images/franchise-banner.jpg",
        "https://previews.123rf.com/images/nadianb/nadianb1707/nadianb170700135/82244490-italian-food-background-pasta-and-meat-long-banner-format-.jpg"
      ],
      eventsData: [
        {
          kapakResim:
            "https://assets.lightspeedhq.com/img/2019/07/8aac85b2-blog_foodpresentationtipsfromtopchefs.jpg",
          name: "Domates soslu makarna",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Kıyma", "Soğan", "Makarna"],
          anaUrun: 1,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 4,
          commentCount: 413,
          isLike: true,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://toriavey.com/images/2010/03/MG_6375-Edit-760x570.jpg",
          name: "Fırında Tavuk",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Biber", "Tavuk", "Patates"],
          anaUrun: 2,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 4.5,
          commentCount: 123,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://i.pinimg.com/originals/2b/aa/30/2baa30d6e0d4bf54848780bc1fa7e734.jpg",
          name: "Hamburger",
          menu: false,
          isLoading: false,
          urunler: ["Kıyma", "Hamburger Ekmeği", "Soğan"],
          anaUrun: 0,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 5.0,
          commentCount: 1231,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://assets.lightspeedhq.com/img/2019/07/8aac85b2-blog_foodpresentationtipsfromtopchefs.jpg",
          name: "Domates soslu makarna",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Kıyma", "Soğan", "Makarna"],
          anaUrun: 1,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 4,
          commentCount: 413,
          isLike: true,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://toriavey.com/images/2010/03/MG_6375-Edit-760x570.jpg",
          name: "Türlü",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Biber", "Tavuk", "Patates"],
          anaUrun: 2,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 1.5,
          commentCount: 123,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://i.pinimg.com/originals/2b/aa/30/2baa30d6e0d4bf54848780bc1fa7e734.jpg",
          name: "Hamburger",
          menu: false,
          isLoading: false,
          urunler: ["Kıyma", "Hamburger Ekmeği", "Soğan"],
          anaUrun: 0,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 5.0,
          commentCount: 1231,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://assets.lightspeedhq.com/img/2019/07/8aac85b2-blog_foodpresentationtipsfromtopchefs.jpg",
          name: "Domates soslu makarna",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Kıyma", "Soğan", "Makarna"],
          anaUrun: 1,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 4,
          commentCount: 413,
          isLike: true,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://toriavey.com/images/2010/03/MG_6375-Edit-760x570.jpg",
          name: "Türlü",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Biber", "Patates", "Kabak"],
          anaUrun: 2,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 1.5,
          commentCount: 123,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://i.pinimg.com/originals/2b/aa/30/2baa30d6e0d4bf54848780bc1fa7e734.jpg",
          name: "Hamburger",
          menu: false,
          isLoading: false,
          urunler: ["Kıyma", "Hamburger Ekmeği", "Soğan"],
          anaUrun: 0,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 5.0,
          commentCount: 1231,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://assets.lightspeedhq.com/img/2019/07/8aac85b2-blog_foodpresentationtipsfromtopchefs.jpg",
          name: "Domates soslu makarna",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Kıyma", "Soğan", "Makarna"],
          anaUrun: 1,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 4,
          commentCount: 413,
          isLike: true,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://toriavey.com/images/2010/03/MG_6375-Edit-760x570.jpg",
          name: "Türlü",
          menu: false,
          isLoading: false,
          urunler: ["Domates", "Biber", "Patates", "Kabak"],
          anaUrun: 2,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 1.5,
          commentCount: 123,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        },
        {
          kapakResim:
            "https://i.pinimg.com/originals/2b/aa/30/2baa30d6e0d4bf54848780bc1fa7e734.jpg",
          name: "Hamburger",
          menu: false,
          isLoading: false,
          urunler: ["Kıyma", "Hamburger Ekmeği", "Soğan"],
          anaUrun: 0,
          actor: {
            fullName: "İsa Yürekli",
            email: "yurekliisa@gmail.com",
            job: "software engineer"
          },
          point: 5.0,
          commentCount: 1231,
          isLike: false,
          summary:
            "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır."
        }
      ]
    };
  },
  methods: {
    onClick(e, item) {
      e.stopPropagation();
      if (item.to || !item.href) return;
      this.$vuetify.goTo(item.href);
    },
    reserve(index) {
      this.eventsData[index].isLoading = true;

      setTimeout(() => (this.eventsData[index].isLoading = false), 2000);
    },
    like(index,val) {
      this.eventsData[index].isLoading = true;
      setTimeout(() => {
        this.eventsData[index].isLoading = false;
        if(!val)
        {
          this.eventsData[index].isLike = true;
        }
        else{
          this.eventsData[index].isLike = false;
        }
      }, 1000);
    }
  }
};
</script>
