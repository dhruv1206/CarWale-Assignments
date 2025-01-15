import React from "react";
import SortingComponent from "./SortingComponent";
import ProductsGrid from "./ProductsGrid";
import "./ListingComponent.css";

export default function ListingComponent() {
  return (
    <div className="listing-component">
      <SortingComponent />
      <ProductsGrid />
    </div>
  );
}
