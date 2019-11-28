import { TestBed } from '@angular/core/testing';

import { RoutingGuardService } from './routing-guard.service';

describe('RoutingGuardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RoutingGuardService = TestBed.get(RoutingGuardService);
    expect(service).toBeTruthy();
  });
});
