import { useState, useContext } from "react";
import "./newPostPage.scss";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import apiRequest from "../../lib/apiRequest";
import UploadWidget from "../../components/uploadWidget/UploadWidget";
import { useNavigate } from "react-router-dom";
import { DropdownContext } from "../../context/DropdownContext";
import { AuthContext } from "../../context/AuthContext";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";


function NewPostPage() {
  const { types, statuses, currencies } = useContext(DropdownContext);
  const { currentUser } = useContext(AuthContext);
  const [value, setValue] = useState("");
  const [images, setImages] = useState([]);
  const [formData, setFormData] = useState({
    title: "",
    price: "",
    currency: 1,
    type: 1,
    status: 1,
    latitude: "",
    longitude: "",
  });
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const res = await apiRequest.post("/Property/Create", {
        title: formData.title,
        description: value,
        typeId: parseInt(formData.type),
        statusId: parseInt(formData.status),
        startDate: new Date(),
        endDate: new Date(),
        price: parseInt(formData.price),
        currencyId: parseInt(formData.currency),
        userId: currentUser.userId,
        longitude: parseFloat(formData.longitude),
        latitude: parseFloat(formData.latitude),
        images: null,
      });

      try {
        const uploadPromises = images.map((image) =>
          apiRequest.post("/Photo/Create", {
            propertyId: res.data.id,
            photoData: image,
          })
        );
        await Promise.all(uploadPromises);
      } catch (err) {
      }
      toast.success("Post created successfully!");
      navigate(`/${res.data.id}`);
      // navigate("/");
    } catch (err) {
      toast.error("Creating post failed. Please try again!");
      setError(error);
    }
  };

  return (
    <div className="newPostPage">
      <div className="formContainer">
        <h1>Add New Post</h1>
        <div className="wrapper">
          <form onSubmit={handleSubmit}>
            <div className="item title">
              <label htmlFor="title">Title</label>
              <input
                id="title"
                name="title"
                type="text"
                value={formData.title}
                onChange={handleInputChange}
              />
            </div>

            <div className="item description">
              <label htmlFor="desc">Description</label>
              <ReactQuill theme="snow" onChange={setValue} value={value} />
            </div>

            <div className="item">
              <label htmlFor="price">Price</label>
              <input
                id="price"
                name="price"
                type="number"
                value={formData.price}
                onChange={handleInputChange}
              />
            </div>

            <div className="item">
              <label htmlFor="currency">Currency</label>
              <select
                name="currency"
                id="currency"
                value={formData.currency}
                onChange={handleInputChange}
              >
                {currencies.map((currency) => (
                  <option key={currency.id} value={currency.id}>
                    {currency.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="item">
              <label htmlFor="type">Type</label>
              <select
                name="type"
                id="type"
                value={formData.type}
                onChange={handleInputChange}
              >
                {types.map((type) => (
                  <option key={type.id} value={type.id}>
                    {type.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="item">
              <label htmlFor="status">Status</label>
              <select
                name="status"
                id="status"
                value={formData.status}
                onChange={handleInputChange}
              >
                {statuses.map((status) => (
                  <option key={status.id} value={status.id}>
                    {status.name}
                  </option>
                ))}
              </select>
            </div>

            <div className="item">
              <label htmlFor="latitude">Latitude</label>
              <input
                id="latitude"
                name="latitude"
                type="text"
                value={formData.latitude}
                onChange={handleInputChange}
              />
            </div>

            <div className="item">
              <label htmlFor="longitude">Longitude</label>
              <input
                id="longitude"
                name="longitude"
                type="text"
                value={formData.longitude}
                onChange={handleInputChange}
              />
            </div>

            <button className="sendButton" type="submit">Create New Post</button>
            {error && <span>{error}</span>}
          </form>
        </div>
      </div>
      <div className="sideContainer">
        {images.map((image, index) => (
          <img src={image} key={index} alt="" />
        ))}
        <UploadWidget
          uwConfig={{
            multiple: true,
            cloudName: "lamadev",
            uploadPreset: "estate",
            folder: "posts",
          }}
          setState={setImages}
        />
      </div>
    </div>
  );
}

export default NewPostPage;
