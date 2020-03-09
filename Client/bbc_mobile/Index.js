import * as React from 'react';
import { StyleSheet, SafeAreaView, View, Text, Image, TouchableOpacity } from 'react-native';
import Constants from 'expo-constants';





export default class App extends React.Component {

    render() {
        return (
            <SafeAreaView style={styles.container}>
                <Image
                    style={{ width: 500, height: 500 }}
                    source={{ uri: 'https://image.freepik.com/free-vector/coloured-chefdesign_1152-72.jpg' }}
                />
                <TouchableOpacity style={styles.button} onPress={() => (this.props.navigation.navigate('Home'))}>
                    <Text style={{ color: '#ffffff', fontWeight: "bold", fontSize: 18 }}>Yemek Tarifleri</Text>
                </TouchableOpacity>

            </SafeAreaView>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        paddingTop: Constants.statusBarHeight,
        justifyContent: "center",
        alignItems: "center"
    },
    button: {
        borderWidth: 1,
        padding: 10,
        borderRadius: 10,
        backgroundColor: "green",
    }











});