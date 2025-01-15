import React, { useEffect, useState } from "react";
import FiltersComponent from "../components/FiltersComponent";
import ListingComponent from "../components/ListingComponent";
import "./HomePage.css";
import { useDispatch, useSelector } from "react-redux";
import { fetchProducts, setInitialFiltersFromUrl } from "../redux/actions";

function HomePage() {
  const { products, priceRange, selectedCategories, priceSort } = useSelector(
    (state) => state
  );

  const dispatch = useDispatch();
  const [firstRun, setFirstRun] = useState(true);
  useEffect(() => {
    const initialFiltersMap = {};
    const url = new URL(window.location.href);
    url.searchParams.forEach((val, key) => {
      initialFiltersMap[key] = val;
    });
    dispatch(setInitialFiltersFromUrl(initialFiltersMap));
  }, []);

  useEffect(() => {
    if (firstRun) {
      setFirstRun(false);
      return;
    }
    dispatch(fetchProducts(selectedCategories, priceRange, priceSort));
  }, [dispatch, priceRange, selectedCategories, priceSort]);

  return (
    <div className="home-container">
      <h2 className="home-title">{`${
        products.length || 0
      } Used Cars in India`}</h2>
      <div className="content-and-filters-container">
        <FiltersComponent />
        <ListingComponent />
      </div>
    </div>
  );
}

export default HomePage;
