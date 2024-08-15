import "./register.scss";
import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import apiRequest from "../../lib/apiRequest";
import { useTranslation } from 'react-i18next';

function Register() {
  const { t } = useTranslation();
  const [errors, setErrors] = useState({});
  const [isLoading, setIsLoading] = useState(false);

  const navigate = useNavigate();

  const validateForm = (formData) => {
    const newErrors = {};

    if (!formData.get("username")) {
      newErrors.username = "Username is required.";
    }

    const email = formData.get("email");
    if (!email) {
      newErrors.email = "Email is required.";
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      newErrors.email = "Invalid email format.";
    }

    if (!formData.get("password")) {
      newErrors.password = "Password is required.";
    }

    return newErrors;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors({});
    setIsLoading(true);
    const formData = new FormData(e.target);

    const validationErrors = validateForm(formData);
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      setIsLoading(false);
      return;
    }

    const firstName = formData.get("username");
    const lastName = formData.get("username");
    const userName = formData.get("username");
    const email = formData.get("email");
    const password = formData.get("password");

    try {
      await apiRequest.post("/Auth/Register", {
        firstName,
        lastName,
        userName,
        email,
        password,
      });

      navigate("/login");
    } catch (err) {
      const apiErrors = [];

      if (err.response && err.response.data) {
        if (Array.isArray(err.response.data)) {
          apiErrors.push(...err.response.data.map(e => e.description));
        } else if (err.response.data.message) {
          apiErrors.push(err.response.data.message);
        }
      } else {
        apiErrors.push("Failed to load resource: the server responded with a status of 400 (Bad Request)");
      }

      setErrors({ api: apiErrors });
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="registerPage">
      <div className="formContainer">
        <form onSubmit={handleSubmit}>
          <h1>{t('create-an-account')}</h1>
          <input name="username" type="text" placeholder="Username" />
          {errors.username && <p className="error">{errors.username}</p>}
          <input name="email" type="text" placeholder="Email" />
          {errors.email && <p className="error">{errors.email}</p>}
          <input name="password" type="password" placeholder="Password" />
          {errors.password && <p className="error">{errors.password}</p>}
          <button disabled={isLoading}>{t('register')}</button>
          {errors.api && (
            <div className="errorContainer">
              {errors.api.map((error, index) => (
                <p key={index} className="error">{error}</p>
              ))}
            </div>
          )}
          <Link to="/login">{t('do-you-have-an-account')}</Link>
        </form>
      </div>
      <div className="imgContainer">
        <img src="/bg.png" alt="" />
      </div>
    </div>
  );
}

export default Register;
