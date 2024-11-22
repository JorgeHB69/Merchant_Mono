"use client";

import { ListType } from "@/commons/entities/ListType";
import { useState } from "react";
import { MdOutlineStar } from "react-icons/md";
import { AddToCart } from "./AddToCart";
import { Like } from "./Like";
import "@/styles/general/ProductCard.css";
import Product from "@/commons/entities/concretes/Product";
import { useModal } from "@/commons/context/ModalContext";
import { ProductPopUp } from "./ProductPopUp";
import Link from "next/link";

interface Props {
  product: Product;
  type: ListType;
}

export const ProductCard = ({ product, type }: Props) => {
  const [isLiked, setIsLiked] = useState(false);
  const { open } = useModal();

  const handleProductClick = () => {
    open(<ProductPopUp currentProduct={product} />);
  };

  return (
    <div className={`product-card ${type}`}>
      <img
        src={product.images[0].url}
        alt={product.images[0].altText}
        className="product-card-image"
      />
      <Link href={`/product/${product.id}`} className="product-card-name">
        {product.name}
      </Link>
      <div className="product-card-info">
        <p className="product-card-price">
          <span className="product-card-price-symbol">$</span> {product.price}
        </p>
        <p className="product-card-rating">
          <MdOutlineStar />{" "}
          {product.productReviews ? product.productReviews.length : 0}
        </p>
      </div>
      <div className="product-card-more-actions">
        <AddToCart product={product} action={handleProductClick} />
        <Like
          productId={product.id}
          isLiked={isLiked}
          toggleLike={() => setIsLiked(!isLiked)}
        />
      </div>
    </div>
  );
};
