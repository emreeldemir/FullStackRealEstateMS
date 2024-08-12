import { useState } from "react";
import "./searchBar.scss";
import { useContext } from "react";
import { DropdownContext } from "../../context/DropdownContext";
import { Link } from "react-router-dom";


function SearchBar() {
  const { types, statuses, currencies } = useContext(DropdownContext);
  const [query, setQuery] = useState({
    type: "",
    status: "",
    currency: "",
  });

  const handleChange = (e) => {
    setQuery((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  return (
    <div className="searchBar">
      <form>
        <select name="type" onChange={handleChange} value={query.type}>
          <option value="">Select Type</option>
          {types.map((type) => (
            <option key={type.id} value={type.id}>
              {type.name}
            </option>
          ))}
        </select>

        <select name="status" onChange={handleChange} value={query.status}>
          <option value="">Select Status</option>
          {statuses.map((status) => (
            <option key={status.id} value={status.id}>
              {status.name}
            </option>
          ))}
        </select>

        <select name="currency" onChange={handleChange} value={query.currency}>
          <option value="">Select Currency</option>
          {currencies.map((currency) => (
            <option key={currency.id} value={currency.id}>
              {currency.name}
            </option>
          ))}
        </select>
        <Link
          to={`/list?type=${query.type}&status=${query.status}&currency=${query.currency}`}
        >
          <button>
            <img src="/search.png" alt="" />
          </button>
        </Link>
      </form>
    </div>
  );
}

export default SearchBar;
