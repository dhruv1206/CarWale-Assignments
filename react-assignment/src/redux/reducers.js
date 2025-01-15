import { fuelCategories } from "../constants";
import {
  CLEAR_FILTERS,
  SET_INITIAL_FILTERS,
  SET_PRICE_SORT,
  SET_PRODUCTS,
} from "./actions";
import { SET_CATEGORIES, TOGGLE_CATEGORY, SET_PRICE_RANGE } from "./actions";

const initialState = {
  categories: fuelCategories,
  selectedCategories: [],
  priceRange: [0, 20],
  products: [],
  priceSort: "asc",
};

const filtersReducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_CATEGORIES:
      return {
        ...state,
        categories: action.payload,
      };
    case SET_PRICE_SORT:
      return {
        ...state,
        priceSort: action.payload,
      };
    case SET_PRODUCTS:
      return {
        ...state,
        products: action.payload,
      };
    case CLEAR_FILTERS:
      return {
        ...state,
        selectedCategories: initialState.selectedCategories,
        priceRange: initialState.priceRange,
        priceSort: initialState.priceSort,
      };
    case TOGGLE_CATEGORY:
      const updatedSelectedCategories = state.selectedCategories.includes(
        action.payload
      )
        ? state.selectedCategories.filter(
            (category) => category !== action.payload
          )
        : [...state.selectedCategories, action.payload];
      return {
        ...state,
        selectedCategories: updatedSelectedCategories,
      };
    case SET_PRICE_RANGE:
      return {
        ...state,
        priceRange: action.payload,
      };
    case SET_INITIAL_FILTERS:
      return {
        ...state,
        ...action.payload,
      };
    default:
      return state;
  }
};

export default filtersReducer;
