import { Link } from "react-router-dom";
import "./card.scss";
import DeleteIcon from '@mui/icons-material/Delete';
import apiRequest from "../../lib/apiRequest";
import { AuthContext } from "../../context/AuthContext";
import { useContext } from "react";

function Card({ item }) {
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
      console.log('Delete successful', response.data);
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
                  <DeleteIcon
                    onClick={() => handleClick(item.id)}  // item.id'yi handleClick'e geçiriyoruz
                    style={{ color: 'red', cursor: 'pointer' }}
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



