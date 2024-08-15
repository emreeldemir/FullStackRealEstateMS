import { useContext } from "react";
import SearchBar from "../../components/searchBar/SearchBar";
import "./homePage.scss";
import { AuthContext } from "../../context/AuthContext";
import { DropdownContextProvider } from "../../context/DropdownContext";
import { useTranslation } from 'react-i18next';

function HomePage() {

  const { currentUser } = useContext(AuthContext)
  const { t } = useTranslation();

  return (
    <div className="homePage">
      <div className="textContainer">
        <div className="wrapper">
          <h1 className="title">{t('homepage-title')}</h1>
          <p>
            {t('homepage-subtitle')}
          </p>
          <DropdownContextProvider>
            <SearchBar />
          </DropdownContextProvider>
          <div className="boxes">
            <div className="box">
              <h1>4+</h1>
              <h2>{t('years-of-experience')}</h2>
            </div>
            <div className="box">
              <h1>23</h1>
              <h2>{t('award-gained')}</h2>
            </div>
            <div className="box">
              <h1>500+</h1>
              <h2>{t('property-ready')}</h2>
            </div>
          </div>
        </div>
      </div>
      <div className="imgContainer">
        <img src="/bg.png" alt="" />
      </div>
    </div>
  );
}

export default HomePage;
