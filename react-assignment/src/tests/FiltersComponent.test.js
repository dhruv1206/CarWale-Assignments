import { render, screen, fireEvent } from "@testing-library/react";
import { Provider, useDispatch, useSelector } from "react-redux";
import FiltersComponent from "../components/FiltersComponent";
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

describe('FiltersComponent', () => {
  it('renders correctly and handles category change', () => {
    useSelector.mockReturnValue({categories: fuelCategories, selectedCategories: [], priceRange:[0, 20]});
    
    render(
      <Provider store={store}>
        <FiltersComponent />
      </Provider>
    );

    const petrolCheckbox = screen.getByLabelText('Petrol');
    fireEvent.click(petrolCheckbox);

    let selectedCategories = store.getState().selectedCategories;
    let isPetrolSelected = selectedCategories.includes('Petrol');
    expect(isPetrolSelected).toBeTruthy();
    fireEvent.click(petrolCheckbox);
    selectedCategories = store.getState().selectedCategories;
    isPetrolSelected = selectedCategories.includes('Petrol');
    expect(isPetrolSelected).toBeFalsy();
  });

  it('clears all filters when Clear All is clicked', () => {
    render(
      <Provider store={store}>
        <FiltersComponent />
      </Provider>
    );
    const petrolCheckbox = screen.getByLabelText('Petrol');
    const dieselCheckbox = screen.getByLabelText('Diesel');

    fireEvent.click(petrolCheckbox);
    fireEvent.click(dieselCheckbox);

    let selectedCategories = store.getState().selectedCategories;
    expect(selectedCategories).toEqual(['Petrol', 'Diesel']);

    const clearButton = screen.getByRole('button', { name: /Clear All/ });
    fireEvent.click(clearButton);

    selectedCategories = store.getState().selectedCategories;
    expect(selectedCategories).toEqual([]);
  });
});
