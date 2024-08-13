import { defer } from "react-router-dom";
import apiRequest from "./apiRequest";

export const singlePageLoader = async ({ request, params }) => {
  const res = await apiRequest("/posts/" + params.id);
  console.log(res);
  return res.data;
};
export const listPageLoader = async ({ request, params }) => {
  console.log(request.url);
  const query = request.url.split("?")[1];
  const postPromise = apiRequest("/Property/Search?" + query);
  const postResponse = await postPromise;
  return defer({
    postResponse: postPromise,
  });
};

export const profilePageLoader = async () => {
  const postPromise = apiRequest("/users/profilePosts");

  return defer({
    postResponse: postPromise,

  });
};
