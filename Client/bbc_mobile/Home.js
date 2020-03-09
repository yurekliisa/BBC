import * as React from 'react';
import {StyleSheet, SafeAreaView, View ,Text, Image,ScrollView} from 'react-native';
import Constants from 'expo-constants';
import { TouchableOpacity } from 'react-native-gesture-handler';




export default class App extends React.Component {

  render() {
    return (
      <SafeAreaView style={styles.container}>
        <View style={styles.header}>
          <View>
            <TouchableOpacity onPress={()=>(this.props.navigation.navigate('Index'))}>
            <Image
          style={{width: 30, height: 30}}
          source={{uri: 'https://image.flaticon.com/icons/png/512/60/60577.png'}}
        />
            </TouchableOpacity>
          
          </View>
          <View>
          <Text style={{fontWeight:"bold",fontSize:22,marginRight:'40%'}}>MENÜLER</Text>
          </View>

        </View>


        <View style={styles.body}>
        <ScrollView>

<TouchableOpacity onPress={()=>(this.props.navigation.navigate('Tarif'))}>
<View style={styles.menu}> 
        <Text style={{fontWeight:"bold"}}>Kullanici Adi: Mevlüt Sözüer</Text>
      <View style={styles.menucontex}>
        <View style={styles.menuimage}>
        <Image
          style={{width: 100, height: 100}}
          source={{uri: 'https://lezzet.blob.core.windows.net/images-xxlarge-recipe/porcini-mantarli-risotto-26bb7904-bb6b-402a-92ad-9aa3be299d61.jpg'}}
        />
        </View>
        <View style={styles.menudiscription}>  
<Text style={{fontWeight:"bold"}}>RISOTTO</Text>
<Text>Risottonun Yapilisi:</Text>
<Text style={{fontSize:12}}>Italyan mutfaginin en popüler yemeklerinden biri olan Risotto, pirinç kullanilarak yapilan klasik bir yemek türüdür.</Text>
        </View>

      </View>
        </View>

</TouchableOpacity>

<TouchableOpacity onPress={()=>(this.props.navigation.navigate('Tarif'))}>
<View style={styles.menu}> 
        <Text style={{fontWeight:"bold"}}>Kullanici Adi: Utku Can Uzun</Text>
      <View style={styles.menucontex}>
        <View style={styles.menuimage}>
        <Image
          style={{width: 100, height: 100}}
          source={{uri: 'https://i.sozcu.com.tr/wp-content/uploads/2019/08/16/iecrop/firinda-tavuk-yemegi-shutter-2_16_9_1565961353.jpg'}}
        />
        </View>
        <View style={styles.menudiscription}>  
<Text style={{fontWeight:"bold"}}>TAVUK FIRINDA</Text>
<Text>Tavuk Firindanin Yapilisi</Text>
<Text style={{fontSize:12}}>Nefis bir marinenin içinde dinlenen tavuk butlari, bol baharatli sebzelerle firinda usul usul pisiyor.</Text>
        </View>

      </View>
        </View>
</TouchableOpacity>
   
<TouchableOpacity onPress={()=>(this.props.navigation.navigate('Tarif'))}>
<View style={styles.menu}> 
        <Text style={{fontWeight:"bold"}}>Kullanici Adi: Ahmet Mithat</Text>
      <View style={styles.menucontex}>
        <View style={styles.menuimage}>
        <Image
          style={{width: 100, height: 100}}
          source={{uri: 'https://i.ytimg.com/vi/Cc6CWMxJQ1A/maxresdefault.jpg'}}
        />
        </View>
        <View style={styles.menudiscription}>  
<Text style={{fontWeight:"bold"}}>SPAGETTI</Text>
<Text>Tavuk Firindanin Yapilisi</Text>
<Text style={{fontSize:12}}>Nefis bir marinenin içinde dinlenen tavuk butlari, bol baharatli sebzelerle firinda usul usul pisiyor.</Text>
        </View>

      </View>
        </View>
</TouchableOpacity>

<TouchableOpacity onPress={()=>(this.props.navigation.navigate('Tarif'))}>
<View style={styles.menu}> 
        <Text style={{fontWeight:"bold"}}>Kullanici Adi: Gökhan Sozüer</Text>
      <View style={styles.menucontex}>
        <View style={styles.menuimage}>
        <Image
          style={{width: 100, height: 100}}
          source={{uri: 'https://i2.haber7.net//fotogaleri/haber7/album/2018/09/mantar_yemekleri_nasil_yapilir_1520080049_9562.jpg'}}
        />
        </View>
        <View style={styles.menudiscription}>  
<Text style={{fontWeight:"bold"}}>MANTAR SOTE</Text>
<Text>Tavuk Firindanin Yapilisi</Text>
<Text style={{fontSize:12}}>Nefis bir marinenin içinde dinlenen tavuk butlari, bol baharatli sebzelerle firinda usul usul pisiyor.</Text>
        </View>

      </View>
        </View>
</TouchableOpacity>

<TouchableOpacity onPress={()=>(this.props.navigation.navigate('Tarif'))}>
<View style={styles.menu}> 
        <Text style={{fontWeight:"bold"}}>Kullanici Adi: Emre Sencan</Text>
      <View style={styles.menucontex}>
        <View style={styles.menuimage}>
        <Image
          style={{width: 100, height: 100}}
          source={{uri: 'https://i4.hurimg.com/i/hurriyet/75/750x422/5afae6ae2269a21f68aee925.jpg'}}
        />
        </View>
        <View style={styles.menudiscription}>  
<Text style={{fontWeight:"bold"}}>KIYMALI PATLICAN</Text>
<Text>Tavuk Firindanin Yapilisi</Text>
<Text style={{fontSize:12}}>Nefis bir marinenin içinde dinlenen tavuk butlari, bol baharatli sebzelerle firinda usul usul pisiyor.</Text>
        </View>

      </View>
        </View>
</TouchableOpacity>
    

        </ScrollView>

        </View>
       
      </SafeAreaView>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor:'#fff',
    paddingTop: Constants.statusBarHeight,
  },
  header:{
    flex:1,
    backgroundColor:'#fff',
    shadowOpacity:0.60,
    flexDirection:"row",
    justifyContent:'space-between',
    alignItems:"center",
    padding:10
  },
  body:{
flex:17,
  },

  menu:{
margin:10,
borderWidth:1,
padding:5,
borderRadius:10
  },
  menucontex:{
    flexDirection:"row",
    marginTop:5
   
  },
  menudiscription:{
    borderWidth:1,
    borderRadius:5,
    width:280,
    height:100,
    padding:5,
    marginLeft:5,
  }
 
  

 






});