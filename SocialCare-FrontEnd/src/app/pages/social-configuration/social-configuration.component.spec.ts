import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SocialConfigurationComponent } from './social-configuration.component';

describe('SocialConfigurationComponent', () => {
  let component: SocialConfigurationComponent;
  let fixture: ComponentFixture<SocialConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SocialConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SocialConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
