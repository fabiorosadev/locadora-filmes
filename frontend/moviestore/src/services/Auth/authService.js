import jwtDecode from 'jwt-decode';
import http, { setJwt } from '../httpService';

const apiAuthEndpoint = '/auth';
const tokenKey = 'token';

export async function login(userName, password) {
  const { data } = await http.post(`${apiAuthEndpoint}/login`, {
    userName,
    password,
  });
  localStorage.setItem(tokenKey, data.token);
}

export async function register(userName, password) {
  const { data } = await http.post(`${apiAuthEndpoint}/register`, {
    userName,
    password,
    role: 'Admin',
  });
  localStorage.setItem(tokenKey, data.token);
}

export function loginWithJwt(jwt) {
  localStorage.setItem(tokenKey, jwt);
}

export function logout() {
  localStorage.removeItem(tokenKey);
}

export function getCurrentUser() {
  try {
    const jwt = localStorage.getItem(tokenKey);
    const decoded = jwtDecode(jwt);
    var current_time = Date.now() / 1000;
    if (decoded.exp < current_time) {
      return null;
    }
    return decoded;
  } catch (ex) {
    return null;
  }
}

export function getJwt() {
  return localStorage.getItem(tokenKey);
}

setJwt(getJwt());

export default {
  login,
  register,
  loginWithJwt,
  logout,
  getCurrentUser,
  getJwt,
};
