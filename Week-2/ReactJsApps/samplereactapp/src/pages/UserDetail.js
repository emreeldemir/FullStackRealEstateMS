import React from "react";
import { useParams } from "react-router-dom";

const UserDetail = () => {
    const { id } = useParams();
    return (
        <div>
            <h2>User Detail Page</h2>
            <p>User ID: {id}</p>
        </div>
    );
}

export default UserDetail;