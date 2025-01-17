import { render, screen, fireEvent } from "@testing-library/react";
import { Provider, useSelector } from "react-redux";
import store from "../store";
import SortingComponent from "../components/SortingComponent";
import { setPriceSort } from "../redux/actions";

jest.mock('react-redux', ()=>{
    const modules = jest.requireActual("react-redux");
    return {
        __esModule: true,
        ...modules,
        useSelector: jest.fn()
    }
});

describe('SortingComponent', () => {
  it('renders correctly and handles price sort change', () => {
    expect(store.getState().priceSort).toBe('asc');
    store.dispatch(setPriceSort("desc"));    
    expect(store.getState().priceSort).toBe('desc');
  });
});
