import { createStore, applyMiddleware } from 'redux';
import {thunk} from 'redux-thunk'; 
import filtersReducer from './redux/reducers';

const store = createStore(filtersReducer, applyMiddleware(thunk));

export default store;
