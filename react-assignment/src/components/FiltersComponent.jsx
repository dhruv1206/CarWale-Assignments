import React, { useEffect } from "react";
import "./FiltersComponent.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFilter } from "@fortawesome/free-solid-svg-icons";
import PriceSlider from "./PriceSlider";
import { useDispatch, useSelector } from "react-redux";
import { clearFilters, toggleCategory } from "../redux/actions";
export default function FiltersComponent() {
  const { categories, selectedCategories } = useSelector((state) => state);
  const dispatch = useDispatch();

  const handleCategoryChange = (category) => {
    dispatch(toggleCategory(category));
  };

  const onFiltersClear = () => {
    dispatch(clearFilters());
  };

  return (
    <div className="filters-component">
      <div className="filters-top">
        <div>
          <FontAwesomeIcon style={{ paddingRight: "5px" }} icon={faFilter} />
          Filters
        </div>
        <button className="filters-link-button" onClick={onFiltersClear}>
          Clear All
        </button>
      </div>
      <div className="filters-multiselect">
        {categories.map((option) => (
          <div key={option}>
            <input
              type="checkbox"
              id={option}
              value={option}
              onChange={(_) => {
                handleCategoryChange(option);
              }}
              checked={selectedCategories.includes(option)}
            />
            <label htmlFor={option}>{option}</label>
          </div>
        ))}
      </div>
      <div className="filters-budget-container">
        <h4
          style={{
            fontWeight: "400",
            margin: 0,
            marginTop: 5,
            marginBottom: 5,
          }}
        >
          Budget
        </h4>
        <PriceSlider />
      </div>
    </div>
  );
}
