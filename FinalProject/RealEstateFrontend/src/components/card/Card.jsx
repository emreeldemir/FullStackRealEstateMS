import { Link } from "react-router-dom";
import "./card.scss";
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import apiRequest from "../../lib/apiRequest";
import { AuthContext } from "../../context/AuthContext";
import { useContext } from "react";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function Card({ item, onDelete }) {
  const { currentUser } = useContext(AuthContext);

  const formatPrice = (price) => {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  };
  const formatDesc = (description) => {
    return description.replace(/<p[^>]*>(.*?)<\/p>/g, '$1');
  };

  const handleClick = async (id) => {
    try {
      const response = await apiRequest.delete(`/Property/Delete/${id}`);
      onDelete(id);
      toast.success("Item deleted successfully!");
    } catch (error) {
      console.error('There was an error deleting the item!', error);
    }
  };

  return (
    <div className="card">
      <Link to={`/${item.id}`} className="imageContainer">
        <img src={item.photos[0]?.photoData} alt="" />
      </Link>

      <div className="textContainer">
        {/* Başlık */}
        <h2 className="title">
          <Link to={`/${item.id}`}>{item.title}</Link>
        </h2>

        {/* Açıklama */}
        <p className="description">
          <span>{formatDesc(item.description)}</span>
        </p>

        {/* Fiyat ve Para Birimi */}
        <p className="price">
          {formatPrice(item.price)} {item.currencyName}
        </p>

        {/* Tür ve Durum (Alt alta yazılması) */}
        {/* <p className="feature">
          <span>{item.typeName}</span>
          <br />
          <span>{item.statusName}</span>
        </p> */}

        <div className="bottom">
          <div className="features">
            <div className="feature">

              <span>{item.typeName}</span>
            </div>
            <div className="feature">

              <span>{item.statusName}</span>
            </div>
          </div>
        </div>

        <div className="bottom">
          <div className="features">
            <div className="listedByContainer">
              <span className="listedBy">Listed by:</span>
              <span className="userName">{item.userName}</span>
            </div>
            {(currentUser.username === "admin" || currentUser.userId === item.userId) && (
              <div className="icons">
                <div className="icon">
                  <DeleteForeverIcon
                    onClick={() => handleClick(item.id)}
                    style={{ color: '#eb233a', cursor: 'pointer' }}
                  />
                </div>
              </div>
            )}
          </div>
        </div>

      </div>
    </div>
  );
}

export default Card;



