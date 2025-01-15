import axios from "axios";
import {
  BASE_URL,
  categoriesNameToNumberMap,
  reverseCategoriesNameToNumberMap,
  STOCKS_ENDPOINT,
} from "../constants";
import { errorFetchingProducts } from "../messages";

export const SET_CATEGORIES = "SET_CATEGORIES";
export const SET_PRODUCTS = "SET_PRODUCTS";
export const TOGGLE_CATEGORY = "TOGGLE_CATEGORY";
export const SET_PRICE_RANGE = "SET_PRICE_RANGE";
export const FETCH_CATEGORIES = "FETCH_CATEGORIES";
export const CLEAR_FILTERS = "CLEAR_FILTERS";
export const SET_INITIAL_FILTERS = "SET_INITIAL_FILTERS";
export const SET_PRICE_SORT = "SET_PRICE_SORT";

export const setCategories = (categories) => ({
  type: SET_CATEGORIES,
  payload: categories,
});

export const setProducts = (products) => ({
  type: SET_PRODUCTS,
  payload: products,
});

export const clearFilters = () => ({
  type: CLEAR_FILTERS,
});

export const toggleCategory = (category) => ({
  type: TOGGLE_CATEGORY,
  payload: category,
});

export const setProductPriceRange = (priceRange) => ({
  type: SET_PRICE_RANGE,
  payload: priceRange,
});

export const setInitialFiltersFromUrl = (initialFiltersMap) => {
  const categoriesNumbers = (initialFiltersMap["fuel"] ?? "").split("+");
  const categories = [];
  if (categoriesNumbers && categoriesNumbers.length > 0) {
    categoriesNumbers.forEach((num) => {
      const cat = reverseCategoriesNameToNumberMap[num];
      if (cat) {
        categories.push(cat);
      }
    });
  }
  const priceRangeArray = (initialFiltersMap["budget"] ?? "").split("-");
  const priceRange = [0, 20];
  if (priceRangeArray && priceRangeArray.length > 0) {
    const start = parseInt(priceRangeArray[0]);
    const end = parseInt(priceRangeArray[1]);
    if (start) {
      priceRange[0] = start;
    }
    if (end) {
      priceRange[1] = end;
    }
  }
  const priceSort = initialFiltersMap["priceSort"] ?? "asc";

  return {
    type: SET_INITIAL_FILTERS,
    payload: {
      selectedCategories: categories,
      priceRange,
      priceSort,
    },
  };
};

export const setPriceSort = (sort) => ({
  type: SET_PRICE_SORT,
  payload: sort,
});

export const fetchProducts =
  (categories = [], priceRange = [0, 20], priceSort = "asc") =>
  async (dispatch) => {
    try {
      let categoryFilterString = "";
      for (const cat of categories) {
        categoryFilterString = `${categoryFilterString}+${categoriesNameToNumberMap[cat]}`;
      }
      categoryFilterString = categoryFilterString.substring(1);
      const queryParams = {
        fuel: categoryFilterString,
        budget: `${priceRange[0]}-${priceRange[1]}`,
        priceSort,
      };

      const url = new URL(window.location.href);
      Object.keys(queryParams).forEach((key) => {
        url.searchParams.set(key, queryParams[key]);
      });
      window.history.pushState({}, "", url);

      const axiosClient = axios.create({
        baseURL: BASE_URL,
        paramsSerializer: {
          encode: (param) => encodeURIComponent(param).replaceAll("%2B", "+"),
        },
      });
      const response = await axiosClient.get(STOCKS_ENDPOINT, {
        params: queryParams,
      });
      if (priceSort === "asc") {
        response.data.stocks = response.data.stocks.sort(
          (a, b) => a.priceNumeric - b.priceNumeric
        );
      } else {
        response.data.stocks = response.data.stocks.sort(
          (a, b) => b.priceNumeric - a.priceNumeric
        );
      }
      dispatch(setProducts(response.data.stocks));
    } catch (error) {
      console.error(errorFetchingProducts, error);
    }
  };
