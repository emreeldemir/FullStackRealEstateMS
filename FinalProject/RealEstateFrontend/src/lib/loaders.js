import { defer } from "react-router-dom";
import apiRequest from "./apiRequest";

export const singlePageLoader = async ({ request, params }) => {
  const res = await apiRequest("/Property/GetPropertyById/" + params.id);
  return res.data;
};

export const listPageLoader = async ({ request, params }) => {
  const query = request.url.split("?")[1];
  const postPromise = await apiRequest("/Property/Search?" + query);
  return defer({
    postResponse: postPromise,
  });
};

export const profilePageLoader = async () => {
  const promise = await apiRequest("/Property/GetAllProperties");

  return defer({
    postResponse: promise,

  });
};
