import React, { useEffect, useState } from "react";
import { InputAdornment, Slider, TextField } from "@mui/material";
import "./PriceSlider.css";
import { useDispatch, useSelector } from "react-redux";
import { setProductPriceRange } from "../redux/actions";
import { expremePriceRangePoints } from "../constants";

export default function PriceSlider() {
  const dispatch = useDispatch();
  const { priceRange: globalPriceRange } = useSelector((state) => state);

  const [priceRange, setPriceRange] = useState([0, 20]);

  const handleSliderChange = (_, newVal) => {
    setPriceRange(newVal);
  };

  const handleMinPriceChange = (event) => {
    const value = Math.min(Number(event.target.value), priceRange[1] - 1);
    setPriceRange([value, priceRange[1]]);
    dispatch(setProductPriceRange([value, priceRange[1]]));
  };

  const handleMaxPriceChange = (event) => {
    const value = Math.max(Number(event.target.value), priceRange[0] + 1);
    setPriceRange([priceRange[0], value]);
    dispatch(setProductPriceRange([priceRange[0], value]));
  };

  const handleSliderChangeRedux = (_, newVal) => {
    dispatch(setProductPriceRange(newVal));
  };

  useEffect(() => {
    setPriceRange(globalPriceRange);
  }, [globalPriceRange]);

  return (
    <>
      <Slider
        style={{
          width: "90%",
          marginLeft: 5,
        }}
        getAriaLabel={() => "Price range"}
        value={priceRange}
        min={expremePriceRangePoints[0]}
        max={expremePriceRangePoints[1]}
        onChange={handleSliderChange}
        onChangeCommitted={handleSliderChangeRedux}
        valueLabelDisplay="auto"
      />

      <div className="slider-subtitle">
        <TextField
          type="number"
          value={priceRange[0]}
          onChange={handleMinPriceChange}
          size="small"
          inputProps={{ min: expremePriceRangePoints[0] }}
          InputProps={{
            endAdornment: <InputAdornment position="end">Lakh</InputAdornment>,
          }}
        />
        <h3 style={{ margin: 0, fontWeight: "bold" }}>-</h3>
        <TextField
          type="number"
          value={priceRange[1]}
          onChange={handleMaxPriceChange}
          size="small"
          inputProps={{
            min: priceRange[0] + 1,
          }}
          InputProps={{
            endAdornment: <InputAdornment position="end">Lakh</InputAdornment>,
          }}
        />
      </div>
    </>
  );
}
