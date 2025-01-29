import { SET_PRODUCTS, TOGGLE_CATEGORY, SET_PRICE_SORT, CLEAR_FILTERS } from "../redux/actions";
import filtersReducer from "../redux/reducers";

describe('filtersReducer', () => {
  it('should handle SET_PRODUCTS', () => {
    const initialState = {
      categories: [],
      selectedCategories: [],
      priceRange: [0, 20],
      products: [],
      priceSort: 'asc'
    };

    const action = {
      type: SET_PRODUCTS,
      payload: [{ id: 1, priceNumeric: 10000, name: 'Car 1' }]
    };

    const newState = filtersReducer(initialState, action);
    expect(newState.products).toEqual(action.payload);
  });

  it('should handle TOGGLE_CATEGORY', () => {
    const initialState = {
      categories: ['SUV', 'Sedan'],
      selectedCategories: ['SUV'],
      priceRange: [0, 20],
      products: [],
      priceSort: 'asc'
    };

    const action = {
      type: TOGGLE_CATEGORY,
      payload: 'Sedan'
    };

    const newState = filtersReducer(initialState, action);
    expect(newState.selectedCategories).toEqual(['SUV', 'Sedan']);
  });

  it('should handle CLEAR_FILTERS', () => {
    const initialState = {
      categories: ['SUV', 'Sedan'],
      selectedCategories: ['SUV', 'Sedan'],
      priceRange: [0, 20],
      products: [],
      priceSort: 'asc'
    };

    const action = { type: CLEAR_FILTERS };
    const newState = filtersReducer(initialState, action);

    expect(newState.selectedCategories).toEqual([]);
    expect(newState.priceRange).toEqual([0, 20]);
    expect(newState.priceSort).toEqual('asc');
  });
});
