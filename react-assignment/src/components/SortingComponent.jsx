import { MenuItem, Select } from "@mui/material";
import React from "react";
import "./SortingComponent.css";
import { useDispatch, useSelector } from "react-redux";
import { setPriceSort } from "../redux/actions";

export default function SortingComponent() {
  const { priceSort } = useSelector((state) => state);
  const dispatch = useDispatch();

  return (
    <div className="sorting-container">
      <p style={{ margin: 0, color: "gray" }}>Sort by:</p>
      <Select
        className="sorting-dropdown"
        value={priceSort}
        onChange={(event) => {
          dispatch(setPriceSort(event.target.value));
        }}
        label="Choose an Option"
        data-testid="sorting-dropdown"
      >
        <MenuItem value="asc">Price (Low to High)</MenuItem>
        <MenuItem value="desc">Price (High to Low)</MenuItem>
      </Select>
    </div>
  );
}
