import { defer } from "react-router-dom";
import { useContext } from "react";
import apiRequest from "./apiRequest";
import { AuthContext } from "../context/AuthContext";



export const singlePageLoader = async ({ request, params }) => {
  const res = await apiRequest("/posts/" + params.id);
  console.log(res);
  return res.data;
};

export const listPageLoader = async ({ request, params }) => {
  console.log(request.url);
  const query = request.url.split("?")[1];
  const postPromise = await apiRequest("/Property/Search?" + query);
  console.log(postPromise);
  return defer({
    postResponse: postPromise,
  });
};

export const profilePageLoader = async () => {
  const promise = await apiRequest("/Property/GetAllProperties");
  console.log(promise);
  //const filteredResults = promise.filter(result => result.userId === 8);

  return defer({
    postResponse: promise,

  });
};
