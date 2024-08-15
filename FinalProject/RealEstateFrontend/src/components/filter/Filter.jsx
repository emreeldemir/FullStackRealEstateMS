import { useState, useEffect } from "react";
import "./filter.scss";
import { useSearchParams } from "react-router-dom";
import { useContext } from "react";
import { DropdownContext } from "../../context/DropdownContext";

function Filter() {
  const { types, statuses, currencies } = useContext(DropdownContext);
  const [searchParams, setSearchParams] = useSearchParams();
  const [query, setQuery] = useState({
    type: searchParams.get("type") || "",
    status: searchParams.get("status") || "",
    currency: searchParams.get("currency") || "",
    minPrice: searchParams.get("minPrice") || "",
    maxPrice: searchParams.get("maxPrice") || "",
  });

  const handleChange = (e) => {
    setQuery({
      ...query,
      [e.target.name]: e.target.value,
    });
  };

  const handleFilter = () => {
    setSearchParams(query);
  };

  return (
    <div className="filter">
      <h1>Search results</h1>
      <div className="bottom">
        <div className="item">
          <label htmlFor="type">Type</label>
          <select
            name="type"
            id="type"
            onChange={handleChange}
            defaultValue={query.type}
          >
            <option value="">All</option>
            {types.map((type) => (
              <option key={type.id} value={type.name}>
                {type.name}
              </option>
            ))}
          </select>
        </div>
        <div className="item">
          <label htmlFor="status">Status</label>
          <select
            name="status"
            id="status"
            onChange={handleChange}
            defaultValue={query.status}
          >
            <option value="">All</option>
            {statuses.map((status) => (
              <option key={status.id} value={status.name}>
                {status.name}
              </option>
            ))}
          </select>
        </div>
        <div className="item">
          <label htmlFor="currency">Currency</label>
          <select
            name="currency"
            id="currency"
            onChange={handleChange}
            defaultValue={query.currency}
          >
            <option value="">All</option>
            {currencies.map((currency) => (
              <option key={currency.id} value={currency.name}>
                {currency.name}
              </option>
            ))}
          </select>
        </div>
        <div className="item">
          <label htmlFor="minPrice">Min Price</label>
          <input
            type="number"
            id="minPrice"
            name="minPrice"
            placeholder="All"
            onChange={handleChange}
            defaultValue={query.minPrice}
          />
        </div>
        <div className="item">
          <label htmlFor="maxPrice">Max Price</label>
          <input
            type="number"
            id="maxPrice"
            name="maxPrice"
            placeholder="All"
            onChange={handleChange}
            defaultValue={query.maxPrice}
          />
        </div>
        <button onClick={handleFilter}>
          <img src="/search.png" alt="Search" />
        </button>
      </div>
    </div>
  );
}

export default Filter;
