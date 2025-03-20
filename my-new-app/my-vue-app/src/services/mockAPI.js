import { mockAPI_URL } from "@/store/util";
import axios from "axios";
import { ref } from "vue";
// import debounce from "lodash/debounce";

const postUsers = ref([]);
const mockAPI = axios.create({
    baseURL: mockAPI_URL,
});

// const fetchPosts = debounce(async (userIds) => {
//     console.log("Fetching posts for user:", userIds);
//     try {
//         let postRequests = [];

//         if (Array.isArray(userIds)) {
//             postRequests = userIds.map((id) => mockAPI.get(`/posts?Id=${id}`));
//         } else {
//             postRequests = [mockAPI.get(`/posts?Id=${userIds}`)];
//         }

//         console.log("Post Requests:", postRequests);

//         const postResponses = await Promise.allSettled(postRequests);

//         console.log("Post Responses:", postResponses);

//         const postUsers = postResponses
//             .filter(res => res.status === "fulfilled")
//             .flatMap(res => res.value.data)
//             .filter(post => Array.isArray(userIds) ? userIds.includes(post.id) : post.id === userIds);

//         console.log("Fetched Post Users:", postUsers);

//         return postUsers;
//     } catch (error) {
//         console.error("Error fetching posts:", error);
//         return [];
//     }
// }, 500); // Debounce for 500ms


// const fetchPosts = debounce(async (userIds) => {
//     console.log("Fetching posts for users:", userIds);
    
//     try {
//         if (!userIds || userIds.length === 0) return [];

//         let postRequests = [];

//         if (Array.isArray(userIds)) {
//             postRequests = userIds.map((id) => mockAPI.get(`/posts?id=${id}`));
//         } else {
//             postRequests = [mockAPI.get(`/posts?id=${userIds}`)];
//         }

//         console.log("Post Requests:", postRequests);

//         const postResponses = await Promise.allSettled(postRequests);

//         console.log("Post Responses:", postResponses);

//         let postUsers = postResponses
//             .filter(res => res.status === "fulfilled") 
//             .flatMap(res => res.value.data) 
//             .filter(post => userIds.includes(post.id));

//         postUsers = postUsers.filter((post, index, self) => 
//             index === self.findIndex(p => p.id === post.id)
//         );

//         console.log("Fetched Unique Post Users:", postUsers);

//         return postUsers;
//     } catch (error) {
//         console.error("Error fetching posts:", error);
//         return [];
//     }
// }, 500); // Debounce for 500ms

const fetchPosts = async (id) => {
    let result = await mockAPI.get(`/posts?id=${id}`)
    console.log(result.data,'resulttttttttttttt');
    return result.data;
}
// const fetchPosts2 = debounce(async (userIds) => {
//     console.log("Fetching posts for users:", userIds);

//     try {
//         if (!userIds || (Array.isArray(userIds) && userIds.length === 0)) return [];
//         // const idsArray = Array.isArray(userIds) ? userIds : [userIds];

//         // let postRequests = idsArray.map((id) => mockAPI.get(`/posts?id=${id}`));

//         const postResponses = await mockAPI.get(`/posts?id=${userIds}`)

//         console.log("Post Responses:", postResponses);

//         let posts = postResponses
//         .filter(res => res.status === "fulfilled" && res.value?.data) 
//         .flatMap(res => {
//             console.log("Response Data:", res.value.data); 
//             return Array.isArray(res.value.data) ? res.value.data : [res.value.data];
//         })
//         .filter(post => Array.isArray(userIds) ? userIds.includes(post.id) : post.id === userIds);

//         // .filter(post => 
            
//         //     post.id && idsArray.includes(post.id));
//         console.log("Fetched Posts:", posts);

//         return Array.isArray(posts) ? posts : []; 
//     } catch (error) {
//         console.error("Error fetching posts:", error);
//         return [];
//     }
// }, 500);

export { mockAPI, fetchPosts, postUsers };