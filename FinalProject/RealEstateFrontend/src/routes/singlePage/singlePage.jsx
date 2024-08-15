import "./singlePage.scss";
import Slider from "../../components/slider/Slider";
import Map from "../../components/map/Map";
import { useLoaderData } from "react-router-dom";
import DOMPurify from "dompurify";
import { useTranslation } from 'react-i18next';

function SinglePage() {
  const { t } = useTranslation();
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
                <div className="price"> {formattedPrice} {post.currencyName}</div>
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

          <p className="title">{t('general')}</p>
          <div className="listVertical">
            <div className="feature">
              <div className="featureText">
                <span>{t('status')}</span>
                <p>{post.statusName}</p>
              </div>
            </div>
            <div className="feature">
              <div className="featureText">
                <span>{t('type')}</span>
                <p>{post.typeName}</p>
              </div>
            </div>
          </div>

          <p className="title">{t('location')}</p>
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
