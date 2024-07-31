import React from "react";

const Users = () => {
    const userList = [
        { id: 1, name: "Emre" },
        { id: 2, name: "Mehmet" },
        { id: 3, name: "Beyza" }];


    return (
        <div>
            <h2>Users Page</h2>
            <ul>
                {userList.map((user) => (
                    <li> {user.name}</li>
                ))}
            </ul>
        </div>
    );

};

export default Users;