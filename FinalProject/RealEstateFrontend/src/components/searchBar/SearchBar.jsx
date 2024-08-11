import { useState } from "react";
import "./searchBar.scss";
import { Link } from "react-router-dom";


function SearchBar() {
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
        <input
          type="text"
          name="type"
          placeholder="Type"
          onChange={handleChange}
        />
        <input
          type="text"
          name="status"
          placeholder="Status"
          onChange={handleChange}
        />
        <input
          type="text"
          name="currency"
          placeholder="Currency"
          onChange={handleChange}
        />
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
