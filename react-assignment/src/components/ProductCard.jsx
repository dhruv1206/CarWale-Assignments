import React from "react";
import "./ProductCard.css";

export default function ProductCard({ product }) {
  return (
    <div className="product-card">
      <img className="product-image" src={product.imageUrl} alt="" />
      <div className="product-details">
        <p className="product-name">{product.carName}</p>
        <p className="product-spec">{`${product.km} kms | ${product.fuel} | ${product.cityName}`}</p>
        <p className="product-price">{`₹${product.price}`}</p>
        <button className="product-seller-details-button">
          {" "}
          Get Seller Details
        </button>
      </div>
    </div>
  );
}
