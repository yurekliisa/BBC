import * as React from 'react';
import { StyleSheet, SafeAreaView, View, Text, Image, ScrollView } from 'react-native';
import Constants from 'expo-constants';
import { TouchableOpacity } from 'react-native-gesture-handler';




export default class App extends React.Component {

    render() {
        return (
            <SafeAreaView style={styles.container}>
                <View style={styles.header}>
                    <View>
                        <TouchableOpacity onPress={() => (this.props.navigation.goBack())}>
                            <Image
                                style={{ width: 30, height: 30 }}
                                source={{ uri: 'https://image.flaticon.com/icons/png/512/60/60577.png' }}
                            />
                        </TouchableOpacity>

                    </View>
                    <View>
                        <Text style={{ fontWeight: "bold", fontSize: 22, marginRight: '35%' }}>MENÜ TARIFI</Text>
                    </View>

                </View>


                <View style={styles.body}>
                    <View style={{flexDirection:"row", marginTop:20,}}>
                        <View style={{borderWidth:5, borderRadius:10}}>
                            <Image
                                style={{ width: 150, height: 150 }}
                                source={{ uri: 'https://lezzet.blob.core.windows.net/images-xxlarge-recipe/porcini-mantarli-risotto-26bb7904-bb6b-402a-92ad-9aa3be299d61.jpg' }}
                            />
                        </View>
                        <View style={{marginLeft:30, marginTop:30}}>
                            <Text style={{fontSize:18,fontWeight:"bold"}}>Malzele Listesi</Text>
                            <Text>.Tuz</Text>
                            <Text>.Biber</Text>
                            <Text>.Tuz</Text>
                            <Text>.Tuz</Text>
                        </View>

                    </View>
                    <View style={{padding:10}}>
                        <Text style={{fontSize:18, fontWeight:"bold"}}>Açiklama</Text>
                        <Text>Rosto adını pişirme tekniğinden alan nefis bir et yemeği. Dana etinin en lezzetli parçalarından biri olan nuarın fırında uzun uzun pişirilmesiyle elde edilen rostonun lezzeti de görüntüsü de o kadar iyi oluyor ki, bu yemeğe en şık sofralarda bile tereddüt etmeden yapabilirsiniz. Çeşitli sebzelerin desteğiyle de lezzeti katlanan dana rostonun tarifini aşağı bırakıyoruz. Mutlaka denemelisiniz… </Text>
                    </View>


                </View>

            </SafeAreaView>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        paddingTop: Constants.statusBarHeight,
    },
    header: {
        flex: 1,
        backgroundColor: '#fff',
        shadowOpacity: 0.60,
        flexDirection: "row",
        justifyContent: 'space-between',
        alignItems: "center",
        padding: 10
    },
    body: {
        flex: 17,
        padding:10
    },












});