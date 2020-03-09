import { createStackNavigator } from 'react-navigation-stack';
import { createAppContainer } from "react-navigation";

import {
  Easing,
  Animated,
  Platform,
  Dimensions,
} from 'react-native';


import HomeScreen from './Home'
import IndexScreen from './Index'
import TarifScreen from './Tarif'


const AppNavigator = createStackNavigator(
  {

    Home: {
      screen: HomeScreen,
    },
    Index:{
      screen:IndexScreen
    },
    
    Tarif:{
      screen:TarifScreen
    }

  

  },

  {
    initialRouteName: 'Index',
    initialRouteParams: { transition: 'fade' },
    headerMode: 'none',
    mode: Platform.OS === 'ios' ? 'modal' : 'card',
    transitionConfig: TransitionConfiguration,
    defaultNavigationOptions: {
      headerVisible: true,
      gesturesEnabled: false,
    },
  }
);



const TransitionConfiguration = () => {
  return {
    transitionSpec: {
      duration: 750,
      easing: Easing.out(Easing.poly(4)),
      timing: Animated.timing,
      useNativeDriver: true,
    },
    screenInterpolator: sceneProps => {
      const { layout, position, scene } = sceneProps;
      const width = layout.initWidth;
      const { index, route } = scene;
      const params = route.params || {}; // <- That's new
      const transition = params.transition || 'default'; // <- That's new
      return {
        collapseExpand: CollapseExpand(index, position),
        default: SlideFromRight(index, position, width),
      }[transition];
    },
  };
};

let CollapseExpand = (index, position) => {
  const inputRange = [index - 1, index, index + 1];
  const opacity = position.interpolate({
    inputRange,
    outputRange: [0, 1, 1],
  });

  const scaleY = position.interpolate({
    inputRange,
    outputRange: [0, 1, 1],
  });

  return {
    opacity,
    transform: [{ scaleY }],
  };
};

let SlideFromRight = (index, position, width) => {
  const inputRange = [index - 1, index, index + 1];
  const translateX = position.interpolate({
    inputRange: [index - 1, index, index + 1],
    outputRange: [width, 0, 0],
  });
  const slideFromRight = { transform: [{ translateX }] };
  return slideFromRight;
};

export default createAppContainer(AppNavigator);
