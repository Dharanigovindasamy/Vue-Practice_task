import { mockAPI_URL } from "@/store/util";
import axios from "axios";
import { ref } from "vue";
const postUsers = ref([]);
const mockAPI = axios.create({
    baseURL: mockAPI_URL,
});

const fetchPosts = async (userIds) => {
    try {
        let postRequests = [];

        if (Array.isArray(userIds)) {
            postRequests = userIds.map((id) => mockAPI.get(`/posts?Id=${id}`));
        } else {
            postRequests = [mockAPI.get(`/posts?Id=${userIds}`)];
        }

        console.log("Post Requests:", postRequests);

        const postResponses = await Promise.allSettled(postRequests);

        console.log("Post Responses:", postResponses);

        const postUsers = postResponses
            .filter(res => res.status === "fulfilled")
            .flatMap(res => res.value.data); 

        console.log("Fetched Post Users:", postUsers);

        return postUsers;
    } catch (error) {
        console.error("Error fetching posts:", error);
        return [];
    }
};


export { mockAPI, fetchPosts, postUsers };