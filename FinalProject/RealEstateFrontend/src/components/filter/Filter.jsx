import { useState, useEffect } from "react";
import axios from "axios";
import "./filter.scss";
import { useSearchParams } from "react-router-dom";
import apiRequest from "../../lib/apiRequest";

function Filter() {
  const [searchParams, setSearchParams] = useSearchParams();
  const [query, setQuery] = useState({
    type: searchParams.get("type") || "",
    status: searchParams.get("status") || "",
    currency: searchParams.get("currency") || "",
    minPrice: searchParams.get("minPrice") || "",
    maxPrice: searchParams.get("maxPrice") || "",
  });

  // Dropdown verilerini saklamak için state'ler
  const [types, setTypes] = useState([]);
  const [statuses, setStatuses] = useState([]);
  const [currencies, setCurrencies] = useState([]);

  // Dropdown menü verilerini API'den çek
  useEffect(() => {
    apiRequest.get("/Type/GetAllTypes")
      .then((response) => {
        setTypes(response.data);
        console.log(response.data);
      })
      .catch((error) => console.error("Error fetching property types:", error));

    apiRequest.get("/Status/GetAllStatuses")
      .then((response) => {
        setStatuses(response.data);
        console.log(response.data);
      })
      .catch((error) => console.error("Error fetching statuses:", error));

    apiRequest.get("/Currency/GetAllCurrencies")
      .then((response) => {
        setCurrencies(response.data);
        console.log(response.data);
      })
      .catch((error) => console.error("Error fetching currencies:", error));
  }, []);

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
            <option value="">Any</option>
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
            <option value="">Any</option>
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
            <option value="">Any</option>
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
            placeholder="Any"
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
            placeholder="Any"
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
