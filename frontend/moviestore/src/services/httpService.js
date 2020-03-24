import axios from 'axios';
import { toast } from 'react-toastify';

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

axios.interceptors.response.use(null, error => {
  const expectedError =
    error.response &&
    error.response.status >= 400 &&
    error.response.status < 500;

  if (!expectedError) {
    const { data } = error.response;
    toast.error(`Ocorreu um erro inesperado: ${data.detail ?? error}`);
  }

  return Promise.reject(error);
});

export function setJwt(jwt) {
  axios.defaults.headers.common = { Authorization: `bearer ${jwt}` };
}

export default {
  get: axios.get,
  post: axios.post,
  put: axios.put,
  delete: axios.delete,
  setJwt,
};
