import "./listPage.scss";
import Filter from "../../components/filter/Filter";
import Card from "../../components/card/Card";
import Map from "../../components/map/Map";
import { Await, useLoaderData, useNavigate } from "react-router-dom";
import { Suspense } from "react";
import { DropdownContextProvider } from "../../context/DropdownContext";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function ListPage() {
  const data = useLoaderData();
  const navigate = useNavigate();

  const onDelete = (id) => {
    toast.success("Item deleted successfully!", {
      autoClose: 800,
      onClose: () => navigate(0)
    });
  };

  return (
    <div className="listPage">
      <div className="listContainer">
        <div className="wrapper">
          <DropdownContextProvider>
            <Filter />
          </DropdownContextProvider>
          <Suspense fallback={<p>Loading...</p>}>
            <Await
              resolve={data.postResponse}
              errorElement={<p>Error loading posts!</p>}
            >
              {(postResponse) =>
                postResponse.data.map((post) => (
                  <Card key={post.id} item={post} onDelete={onDelete} />
                ))
              }
            </Await>
          </Suspense>
        </div>
      </div>
      <div className="mapContainer">
        <Suspense fallback={<p>Loading...</p>}>
          <Await
            resolve={data.postResponse}
            errorElement={<p>Error loading posts!</p>}
          >
            {(postResponse) => <Map items={postResponse.data} />}
          </Await>
        </Suspense>
      </div>
    </div>
  );
}

export default ListPage;
