import http from '../httpService';

const apiGenresEndpoint = '/genres';

function genreUrl(id) {
  return `${apiGenresEndpoint}/${id}`;
}

export function getGenres() {
  return http.get(apiGenresEndpoint);
}

export function getGenre(id) {
  return http.get(genreUrl(id));
}

export function saveGenre(genre) {
  if (genre.id) {
    return http.put(apiGenresEndpoint, genre);
  }
  return http.post(apiGenresEndpoint, genre);
}

export function deleteGenre(id) {
  return http.delete(genreUrl(id));
}

export function deleteGenres(genreIds) {
  return http.post(`${apiGenresEndpoint}/RemoveMultiple`, genreIds);
}
