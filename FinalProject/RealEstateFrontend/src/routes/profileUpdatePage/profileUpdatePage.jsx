import { useContext, useState } from "react";
import "./profileUpdatePage.scss";
import { AuthContext } from "../../context/AuthContext";
import apiRequest from "../../lib/apiRequest";
import { useNavigate } from "react-router-dom";

function ProfileUpdatePage() {
  const { currentUser, updateUser } = useContext(AuthContext);
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);

    const { userName, email, newPassword } = Object.fromEntries(formData);

    const request = {
      userId: currentUser.userId,
      userName,
      email,
      newPassword
    };

    try {
      const res = await apiRequest.put("/Auth/UpdateUserInfo", request);
      if (res && res.data) {
        updateUser(null);
        navigate("/");
      } else {
        setError("Unexpected response format");
      }
    } catch (err) {
      setError(err.response.data.message);
    }
  };

  return (
    <div className="profileUpdatePage">
      <div className="formContainer">
        <form onSubmit={handleSubmit}>
          <h1>Update Profile</h1>
          <div className="item">
            <label htmlFor="userName">Username</label>
            <input
              id="userName"
              name="userName"
              type="text"
              defaultValue={currentUser.userName}
            />
          </div>
          <div className="item">
            <label htmlFor="email">Email</label>
            <input
              id="email"
              name="email"
              type="email"
              defaultValue={currentUser.email}
            />
          </div>
          <div className="item">
            <label htmlFor="newPassword">Password</label>
            <input id="newPassword" name="newPassword" type="password" />
          </div>

          <button>Update</button>
          {error && <span>error</span>}
        </form>
      </div>
    </div>
  );
}

export default ProfileUpdatePage;
