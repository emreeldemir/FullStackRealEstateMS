import { Link } from "react-router-dom";
import "./card.scss";

function Card({ item }) {
  // Fiyatı formatlama fonksiyonu
  const formatPrice = (price) => {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
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
          <span>{item.description}</span>
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
            <div className="icons">
              <div className="icon">
                <img src="/save.png" alt="" />
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  );
}

export default Card;



