import { useContext, useState } from "react";
import "./navbar.scss";
import { Link } from "react-router-dom";
import { AuthContext } from "../../context/AuthContext";
import React from 'react';
import { useTranslation } from 'react-i18next';
import LanguageSwitcher from "../languageSwitcher/LanguageSwitcher";

function Navbar() {
  const [open, setOpen] = useState(false);
  const { currentUser } = useContext(AuthContext);
  const { t } = useTranslation();

  return (
    <nav>
      <div className="left">
        <a href="/" className="logo">
          <img src="/logo.png" alt="" />
          <span>Emre Estate</span>
        </a>
        <a href="/">{t('home')}</a>
        <a href="/">{t('about')}</a>
        <a href="/">{t('contact')}</a>
      </div>
      <div className="right">
        {currentUser ? (
          <div className="user">
            <img src={currentUser.username === "admin" ? "/adminAvatar.png" : "/userAvatar3.jpg"} alt="" />
            <span>{currentUser.username}</span>
            <Link to="/profile" className="profile">
              <span>{t('profile')}</span>
            </Link>
            <LanguageSwitcher />
          </div>
        ) : (
          <>
            <a href="/login">{t('sign in')}</a>
            <a href="/register" className="register">
              {t('sign up')}
            </a>
          </>
        )}
        <div className="menuIcon">
          <img
            src="/menu.png"
            alt=""
            onClick={() => setOpen((prev) => !prev)}
          />
        </div>
        <div className={open ? "menu active" : "menu"}>
          <a href="/">Home</a>
          {/* <a href="/">About</a>
          <a href="/">Contact</a>
          <a href="/">Agents</a> */}
          <a href="/">Sign in</a>
          <a href="/">Sign up</a>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
