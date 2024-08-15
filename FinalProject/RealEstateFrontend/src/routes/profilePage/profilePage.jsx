import List from "../../components/list/List";
import "./profilePage.scss";
import { Await, Link, useLoaderData, useNavigate } from "react-router-dom";
import { Suspense, useContext } from "react";
import { AuthContext } from "../../context/AuthContext";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useTranslation } from 'react-i18next';


function ProfilePage() {
  const { t } = useTranslation();
  const data = useLoaderData();
  const { updateUser, currentUser } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleLogout = async () => {
    updateUser(null);
    toast.success("Logged out!");
    navigate("/");
  };

  return (
    <div className="profilePage">
      <div className="details">
        <div className="wrapper">
          <div className="title">
            <h1 style={{ fontWeight: 'bold' }}>{t('user-info')}</h1>
            <Link to="/profile/update">
              <button>{t('update-profile')}</button>
            </Link>
          </div>
          <div className="info">
            <span>
              {t('username')}: <b>{currentUser.username}</b>
            </span>
            <span>
              {t('email')}: <b>{currentUser.email}</b>
            </span>
            <button onClick={handleLogout}>{t('logout')}</button>
          </div>
          <div className="title">
            <h1 style={{ fontWeight: 'bold' }}>{t('my-list')}</h1>
            <Link to="/add">
              <button>{t('create-new-post')}</button>
            </Link>
          </div>
          <Suspense fallback={<p>Loading...</p>}>
            <Await
              resolve={data}
              errorElement={<p>Error loading posts!</p>}
            >
              {(postResponse) => <List posts={data} />}
            </Await>
          </Suspense>
        </div>
      </div>
    </div>
  );
}

export default ProfilePage;
