import "./singlePage.scss";
import Slider from "../../components/slider/Slider";
import Map from "../../components/map/Map";
import { useLoaderData } from "react-router-dom";
import DOMPurify from "dompurify";

function SinglePage() {
  const post = useLoaderData();
  const photoUrls = post.photos.map(photo => photo.photoData);

  const formatPrice = (price) => {
    return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  };

  const formattedPrice = formatPrice(post.price);

  return (
    <div className="singlePage">
      <div className="details">
        <div className="wrapper">

          <Slider images={photoUrls} />

          <div className="info">
            <div className="top">
              <div className="post">
                <h1>{post.title}</h1>
                <div className="price">$ {formattedPrice}</div>
              </div>
              <div className="user">
                <img src="/userAvatar3.jpg" alt="" />
                <span>{post.userName}</span>
              </div>
            </div>
            <div
              className="bottom"
              dangerouslySetInnerHTML={{
                __html: DOMPurify.sanitize(post.description),
              }}
            ></div>
          </div>

        </div>
      </div>

      <div className="features">

        <div className="wrapper">

          <p className="title">General</p>
          <div className="listVertical">
            <div className="feature">
              <div className="featureText">
                <span>Status</span>
                <p>{post.statusName}</p>
              </div>
            </div>
            <div className="feature">
              <div className="featureText">
                <span>Type</span>
                <p>{post.typeName}</p>
              </div>
            </div>
          </div>

          <p className="title">Location</p>
          <div className="mapContainer">
            <Map items={[post]} />
          </div>

          <div className="buttons">
          </div>
        </div>

      </div>
    </div>

  );
}

export default SinglePage;
