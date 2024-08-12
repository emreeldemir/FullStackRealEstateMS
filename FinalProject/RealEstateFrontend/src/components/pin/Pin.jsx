import { Marker, Popup } from "react-leaflet";
import "./pin.scss";
import { Link } from "react-router-dom";

function Pin({ item }) {
  // Fiyatı formatlama fonksiyonu
  const formatPrice = (price) => {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  };

  return (
    <Marker position={[item.latitude, item.longitude]}>
      <Popup>
        <div className="popupContainer">
          <img src={item.photos[0]?.photoData} alt="" />
          <div className="textContainer">
            <Link to={`/${item.id}`}>{item.title}</Link>
            {/* Fiyatı formatlayarak gösteriyoruz */}
            <b>$ {formatPrice(item.price)}</b>
          </div>
        </div>
      </Popup>
    </Marker>
  );
}

export default Pin;
