import { createContext, useEffect, useState } from "react";
import apiRequest from "../lib/apiRequest";

export const DropdownContext = createContext();

export const DropdownContextProvider = ({ children }) => {

    const [types, setTypes] = useState([]);
    const [statuses, setStatuses] = useState([]);
    const [currencies, setCurrencies] = useState([]);

    useEffect(() => {
        apiRequest.get("/Type/GetAllTypes")
            .then((response) => {
                setTypes(response.data);
            })
            .catch((error) => console.error("Error fetching property types:", error));

        apiRequest.get("/Status/GetAllStatuses")
            .then((response) => {
                setStatuses(response.data);
            })
            .catch((error) => console.error("Error fetching statuses:", error));

        apiRequest.get("/Currency/GetAllCurrencies")
            .then((response) => {
                setCurrencies(response.data);
            })
            .catch((error) => console.error("Error fetching currencies:", error));
    }, []);


    return (
        <DropdownContext.Provider value={{ types, statuses, currencies }}>
            {children}
        </DropdownContext.Provider>
    );
};











