import { useState, useContext, useEffect } from "react";
import { useParams } from "react-router-dom";
import "../newPostPage/newPostPage.scss";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import apiRequest from "../../lib/apiRequest";
import { useNavigate } from "react-router-dom";
import { DropdownContext } from "../../context/DropdownContext";
import { AuthContext } from "../../context/AuthContext";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useTranslation } from 'react-i18next';


function PropertyUpdatePage() {
    const { t } = useTranslation();
    const { id } = useParams();
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

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await apiRequest(`/Property/GetPropertyById/${id}`);
                const property = response.data;
                setFormData({
                    title: property.title,
                    price: property.price,
                    currency: property.currencyId,
                    type: property.typeId,
                    status: property.statusId,
                    latitude: property.latitude,
                    longitude: property.longitude,
                });
                setValue(property.description);
                setImages(property.photos.map(photo => photo.photoData));
            } catch (error) {
                console.error('There was an error fetching the property!', error);
            }
        };

        fetchData();
    }, [id]);

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
            const res = await apiRequest.put(`/Property/Update/${id}`, {
                title: formData.title,
                description: value,
                typeId: formData.type,
                statusId: formData.status,
                startDate: new Date(),
                endDate: new Date(),
                price: parseInt(formData.price),
                currencyId: formData.currency,
                userId: currentUser.userId,
                longitude: parseFloat(formData.longitude),
                latitude: parseFloat(formData.latitude),
            });
            navigate(`/${res.data.id}`);

        } catch (err) {
            toast.error("Update post failed. Please try again!");
            setError(error);
        }
        toast.success("Post updated successfully!");

    };

    return (
        <div className="newPostPage">
            <div className="formContainer">
                <h1>{t('update-post')}</h1>
                <div className="wrapper">
                    <form onSubmit={handleSubmit}>
                        <div className="item title">
                            <label htmlFor="title">{t('title')}</label>
                            <input
                                id="title"
                                name="title"
                                type="text"
                                value={formData.title}
                                onChange={handleInputChange}
                            />
                        </div>

                        <div className="item description">
                            <label htmlFor="desc">{t('description')}</label>
                            <ReactQuill theme="snow" onChange={setValue} value={value} />
                        </div>

                        <div className="item">
                            <label htmlFor="price">{t('price')}</label>
                            <input
                                id="price"
                                name="price"
                                type="number"
                                value={formData.price}
                                onChange={handleInputChange}
                            />
                        </div>

                        <div className="item">
                            <label htmlFor="currency">{t('currency')}</label>
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
                            <label htmlFor="type">{t('type')}</label>
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
                            <label htmlFor="status">{t('status')}</label>
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
                            <label htmlFor="latitude">{t('latitude')}</label>
                            <input
                                id="latitude"
                                name="latitude"
                                type="text"
                                value={formData.latitude}
                                onChange={handleInputChange}
                            />
                        </div>

                        <div className="item">
                            <label htmlFor="longitude">{t('longitude')}</label>
                            <input
                                id="longitude"
                                name="longitude"
                                type="text"
                                value={formData.longitude}
                                onChange={handleInputChange}
                            />
                        </div>

                        <button className="sendButton" type="submit">Update Property</button>
                        {error && <span>{error}</span>}
                    </form>
                </div>
            </div>
        </div>
    );
}

export default PropertyUpdatePage;
