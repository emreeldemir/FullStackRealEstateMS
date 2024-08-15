import { useState, useEffect } from "react";
import "./filter.scss";
import { useSearchParams } from "react-router-dom";
import { useContext } from "react";
import { DropdownContext } from "../../context/DropdownContext";
import { useTranslation } from 'react-i18next';

function Filter() {
  const { t } = useTranslation();
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
      <h1>{t('search-results')}</h1>
      <div className="bottom">
        <div className="item">
          <label htmlFor="type">{t('type')}</label>
          <select
            name="type"
            id="type"
            onChange={handleChange}
            defaultValue={query.type}
          >
            <option value="">{t('all')}</option>
            {types.map((type) => (
              <option key={type.id} value={type.name}>
                {type.name}
              </option>
            ))}
          </select>
        </div>
        <div className="item">
          <label htmlFor="status">{t('status')}</label>
          <select
            name="status"
            id="status"
            onChange={handleChange}
            defaultValue={query.status}
          >
            <option value="">{t('all')}</option>
            {statuses.map((status) => (
              <option key={status.id} value={status.name}>
                {status.name}
              </option>
            ))}
          </select>
        </div>
        <div className="item">
          <label htmlFor="currency">{t('currency')}</label>
          <select
            name="currency"
            id="currency"
            onChange={handleChange}
            defaultValue={query.currency}
          >
            <option value="">{t('all')}</option>
            {currencies.map((currency) => (
              <option key={currency.id} value={currency.name}>
                {currency.name}
              </option>
            ))}
          </select>
        </div>
        <div className="item">
          <label htmlFor="minPrice">{t('min-price')}</label>
          <input
            type="number"
            id="minPrice"
            name="minPrice"
            placeholder={t('all')}
            onChange={handleChange}
            defaultValue={query.minPrice}
          />
        </div>
        <div className="item">
          <label htmlFor="maxPrice">{t('max-price')}</label>
          <input
            type="number"
            id="maxPrice"
            name="maxPrice"
            placeholder={t('all')}
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
