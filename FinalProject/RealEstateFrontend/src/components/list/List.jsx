import './list.scss'
import Card from "../card/Card"
import { AuthContext } from "../../context/AuthContext";
import { useState, useContext } from "react";


function List({ posts }) {
  const { currentUser } = useContext(AuthContext);
  const filteredPosts = posts.postResponse.data.filter(item => item.userId === currentUser.userId);
  return (
    <div className='list'>
      {filteredPosts.map(item => (
        <Card key={item.id} item={item} />
      ))}
    </div>
  )
}

export default List