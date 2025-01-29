import { render, screen } from "@testing-library/react";
import { Provider, useDispatch, useSelector } from "react-redux";
import HomePage from "../pages/HomePage";
import { fuelCategories } from "../constants";
import store from "../store";

jest.mock('react-redux', ()=>{
    const modules = jest.requireActual("react-redux");
    return {
        __esModule: true,
        ...modules,
        useSelector: jest.fn()
    }
});

describe('HomePage Component', () => {
  it('renders correctly and shows product count', () => {
      const products = [
          { id: 1, name: 'Car 1', priceNumeric: 10000, imageUrl: 'car1.jpg' },
          { id: 2, name: 'Car 2', priceNumeric: 15000, imageUrl: 'car2.jpg' },
        
        ];
    
    useSelector.mockReturnValue({products, categories: fuelCategories, priceRange: [0, 20], selectedCategories: [], priceSort: "asc"});
    render(
        <Provider store={store}>
            <HomePage />
        </Provider>
    );
    let text = screen.queryByText(/Used Cars in India/)?.innerHTML ?? null;
    expect(text).toBeDefined();
    text = screen.queryByText(/2 Used Cars in India/)?.innerHTML ?? "";
    expect(text).toBe('2 Used Cars in India');
  });
});
