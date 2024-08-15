import './list.scss'
import Card from "../card/Card"
import { AuthContext } from "../../context/AuthContext";
import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function List({ posts }) {

  const navigate = useNavigate();

  const onDelete = (id) => {
    toast.success("Item deleted successfully!", {
      autoClose: 800,
      onClose: () => navigate(0)
    });
  };

  const { currentUser } = useContext(AuthContext);
  const filteredPosts = posts.postResponse.data.filter(item => item.userId === currentUser.userId);
  return (
    <div className='list'>
      {filteredPosts.map(item => (
        <Card key={item.id} item={item} onDelete={onDelete} />
      ))}
    </div>
  )
}

export default List