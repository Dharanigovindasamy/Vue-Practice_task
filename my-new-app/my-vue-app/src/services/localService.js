import { LOCAL_API_URL } from "@/store/util";
import axios from "axios";

//const usersData = ref([]);
const localAPI = axios.create({
    baseURL: LOCAL_API_URL,
});
const fetchUsers = async () => {
    try {
        const response = await localAPI.get("/users");
      //  usersData.value = response.data;
        console.log("User data fetched:",response.data);
        return response.data;
    } catch (error) {
        console.error("Error fetching users:", error);
        return []; 
    }
};
export { localAPI, fetchUsers };