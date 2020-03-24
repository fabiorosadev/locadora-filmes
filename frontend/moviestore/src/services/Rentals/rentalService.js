import http from '../httpService';

const apiRentalEndpoint = '/rental';

function rentalUrl(id) {
  return `${apiRentalEndpoint}/${id}`;
}

export function getRentals() {
  return http.get(apiRentalEndpoint);
}

export function getRental(id) {
  return http.get(rentalUrl(id));
}

export function saveRental(rental) {
  if (rental.id) {
    return http.put(apiRentalEndpoint, rental);
  }

  return http.post(apiRentalEndpoint, rental);
}

export function deleteRental(id) {
  return http.delete(rentalUrl(id));
}

export function deleteRentals(rentalIds) {
  return http.post(`${apiRentalEndpoint}/RemoveMultiple`, rentalIds);
}
