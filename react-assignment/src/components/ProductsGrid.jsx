import React from "react";
import ProductCard from "./ProductCard";
import "./ProductsGrid.css";
import { useSelector } from "react-redux";

export default function ProductsGrid() {
  const { products } = useSelector((state) => state);
  return (
    <div className="products-list">
      {products
        .filter((product) => product.imageUrl)
        .map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
    </div>
  );
}
