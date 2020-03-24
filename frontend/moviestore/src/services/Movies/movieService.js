import http from '../httpService';

const apiMovieEndpoint = '/movies';

function movieUrl(id) {
  return `${apiMovieEndpoint}/${id}`;
}

export function getMovies() {
  return http.get(apiMovieEndpoint);
}

export function getMovie(id) {
  return http.get(movieUrl(id));
}

export function saveMovie(movie) {
  if (movie.id) {
    return http.put(apiMovieEndpoint, movie);
  }

  return http.post(apiMovieEndpoint, movie);
}

export function deleteMovie(id) {
  return http.delete(movieUrl(id));
}

export function deleteMovies(movieIds) {
  return http.post(`${apiMovieEndpoint}/RemoveMultiple`, movieIds);
}
