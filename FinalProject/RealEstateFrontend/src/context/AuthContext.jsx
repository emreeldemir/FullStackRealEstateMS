import { createContext, useEffect, useState } from "react";

export const AuthContext = createContext();

export const AuthContextProvider = ({ children }) => {

  const [currentUser, setCurrentUser] = useState(
    JSON.parse(localStorage.getItem("token")) || null
  );

  const updateUser = (data) => {
    setCurrentUser(data?.token || null);  // Burada `data` kontrol ediliyor
    console.log("Token:", data?.token || "No token");
  };

  useEffect(() => {
    if (currentUser) {
      localStorage.setItem("token", JSON.stringify(currentUser));
    } else {
      localStorage.removeItem("token");
    }
  }, [currentUser]);


  return (
    <AuthContext.Provider value={{ currentUser, updateUser }}>
      {children}
    </AuthContext.Provider>
  );
};
